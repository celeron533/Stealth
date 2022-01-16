using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvoke;
using System.Diagnostics;

namespace Stealth.Core
{
    public class WindowInstanceInfo
    {
        #region Basic Info

        /// <summary>
        /// Window hWnd
        /// </summary>
        public readonly IntPtr HWnd;

        /// <summary>
        /// Window Title
        /// </summary>
        public string Title;

        /// <summary>
        /// Is this window Visible or not
        /// </summary>
        public bool IsVisible;

        /// <summary>
        /// User32.WINDOWINFO, contains window information
        /// </summary>
        public User32.WINDOWINFO WindowInfo;
        #endregion

        #region Detailed Info
        private int _extendedStyle;

        private bool _isTopMostChanged;
        private bool _isTopMost;
        public bool IsTopMost
        {
            get { return _isTopMost; }
            set
            {
                if (_isTopMostChanged = (_isTopMost != value))
                    _isTopMost = value;
            }
        }

        private bool _isLayeredChanged;
        private bool _isLayered;
        public bool IsLayered
        {
            get { return _isLayered; }
            set
            {
                if (_isLayeredChanged = (_isLayered != value))
                    _isLayered = value;
            }
        }

        public bool _crKeyChanged;
        private uint _crkey;
        public uint CrKey
        {
            get { return _crkey; }
            set
            {
                if (_crKeyChanged = (_crkey != value))
                    _crkey = value;
            }
        }

        public bool _bAlphaChanged;
        private byte _bAlpha;
        public byte BAlpha //uint
        {
            get { return _bAlpha; }
            set
            {
                if (_bAlphaChanged = (_bAlpha != value))
                    _bAlpha = value;
            }
        }

        public bool _dwFlagsChanged;
        private uint _dwFlags;
        public uint DwFlags
        {
            get { return _dwFlags; }
            set
            {
                if (_dwFlagsChanged = (_dwFlags != value))
                    _dwFlags = value;
            }
        }

        public Process process
        {
            get;
            set;
        }

        #endregion

        public WindowInstanceInfo(IntPtr hWnd) : base()
        {
            this.HWnd = hWnd;
            GetBasicInfo();
        }

        /// <summary>
        /// Get basic info from window
        /// </summary>
        public void GetBasicInfo()
        {
            IsVisible = User32.IsWindowVisible(HWnd);
            char[] t = new char[255];
            // User32.GetWindowText(hWnd); may have some exceptions when accessing system processes
            User32.GetWindowText(HWnd, t, t.Length + 1);
            Title =
                t[0] == '\0' ? string.Empty : new string(t).Trim('\0');
            User32.GetWindowInfo(HWnd, ref WindowInfo);
        }

        /// <summary>
        /// Get more detailed details from window
        /// </summary>
        public void GetDetailedInfo()
        {
            //windowInfo.dwExStyle: you must set WS_EX_LAYERED before using window alpha
            //transparency  https://msdn.microsoft.com/en-us/library/windows/desktop/ms632599(v=vs.85).aspx#layered

            // Get opacity
            uint tempCrKey, tempDwFlags;
            byte tempBAlpha;
            NativeMethods.GetLayeredWindowAttributes(HWnd, out tempCrKey, out tempBAlpha, out tempDwFlags);
            CrKey = tempCrKey;
            BAlpha = tempBAlpha;
            DwFlags = tempDwFlags;

            // Get IsLayered. Opacity works when IsLayered = true
            _extendedStyle = User32.GetWindowLong(HWnd, User32.WindowLongIndexFlags.GWL_EXSTYLE);
            IsLayered = (_extendedStyle & (int)User32.SetWindowLongFlags.WS_EX_LAYERED) != 0;
            IsTopMost = (_extendedStyle & (int)User32.SetWindowLongFlags.WS_EX_TOPMOST) != 0;

            try
            {
                process = Process.GetProcessById((int)HWnd);
            }
            catch(ArgumentException)
            {
                // normally caused by process not running
            }
            catch(Exception)
            {
                // other unknown exceptions
            }
        }

        /// <summary>
        /// Commit changes to the window
        /// </summary>
        public void CommitChanges()
        {
            if (_isTopMostChanged)
            {
                _isTopMostChanged = false;
                if (IsTopMost)
                {
                    User32.SetWindowPos(HWnd,
                                        User32.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 0, 0,
                                        User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE | User32.SetWindowPosFlags.SWP_NOACTIVATE);
                }
                else
                {
                    User32.SetWindowPos(HWnd,
                                        User32.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 0, 0,
                                        User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE);
                }
            }

            if (_isLayeredChanged)
            {
                _isLayeredChanged = false;
                SetBitFlag(ref _extendedStyle, (int)User32.SetWindowLongFlags.WS_EX_LAYERED, IsLayered);
                User32.SetWindowLong(HWnd, User32.WindowLongIndexFlags.GWL_EXSTYLE, (User32.SetWindowLongFlags)_extendedStyle);
            }

            if (_crKeyChanged || _bAlphaChanged || _dwFlagsChanged)
            {
                _crKeyChanged = false;
                _bAlphaChanged = false;
                _dwFlagsChanged = false;
                NativeMethods.SetLayeredWindowAttributes(HWnd, CrKey, BAlpha, DwFlags);
            }
        }


        /// <summary>
        /// Set or remove a specified bit in the bit flags
        /// </summary>
        /// <param name="sourceBits">ref int</param>
        /// <param name="bitMask">int</param>
        /// <param name="value">bool</param>
        private void SetBitFlag(ref int sourceBits, int bitMask, bool value)
        {
            if (value)
                //   sourceBits 1011[0]11
                //   bitMask    0000[1]00
                //      OR      1011[1]11
                sourceBits |= bitMask;
            else
                //   sourceBits 1011[1]11
                //   ~bitMask   1111[0]11
                //      AND     1011[0]11
                sourceBits &= ~bitMask;
        }


        public override string ToString()
        {
            return $"hWnd={HWnd}, title={Title}, isVisible={IsVisible}, crKey={CrKey}, bAlpha={BAlpha}, dwFlags={DwFlags}, isLayered={IsLayered}";
        }
    }
}
