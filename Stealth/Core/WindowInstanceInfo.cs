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
        public readonly IntPtr hWnd;
        public string title;
        public bool isVisible;
        public User32.WINDOWINFO windowInfo;
        #endregion

        #region Detailed Info
        private int _extendedStyle;

        private bool _isTopMostChanged;
        private bool _isTopMost;
        public bool isTopMost
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
        public bool isLayered
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
        public uint crKey
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
        public byte bAlpha //uint
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
        public uint dwFlags
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
            this.hWnd = hWnd;
            GetBasicInfo();
        }

        /// <summary>
        /// Get basic info from window
        /// </summary>
        public void GetBasicInfo()
        {
            isVisible = User32.IsWindowVisible(hWnd);
            char[] t = new char[255];
            // User32.GetWindowText(hWnd); may have some exceptions when accessing system processes
            User32.GetWindowText(hWnd, t, t.Length + 1);
            title =
                t[0] == '\0' ? string.Empty : new string(t).Trim('\0');
            User32.GetWindowInfo(hWnd, ref windowInfo);
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
            NativeMethods.GetLayeredWindowAttributes(hWnd, out tempCrKey, out tempBAlpha, out tempDwFlags);
            crKey = tempCrKey;
            bAlpha = tempBAlpha;
            dwFlags = tempDwFlags;

            // Get IsLayered. Opacity works when IsLayered = true
            _extendedStyle = User32.GetWindowLong(hWnd, User32.WindowLongIndexFlags.GWL_EXSTYLE);
            isLayered = (_extendedStyle & (int)User32.SetWindowLongFlags.WS_EX_LAYERED) != 0;
            isTopMost = (_extendedStyle & (int)User32.SetWindowLongFlags.WS_EX_TOPMOST) != 0;

            try
            {
                process = Process.GetProcessById((int)hWnd);
            }
            catch
            { }
        }

        /// <summary>
        /// Commit changes to the window
        /// </summary>
        public void CommitChanges()
        {
            if (_isTopMostChanged)
            {
                _isTopMostChanged = false;
                if (isTopMost)
                {
                    User32.SetWindowPos(hWnd,
                                        User32.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 0, 0,
                                        User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE | User32.SetWindowPosFlags.SWP_NOACTIVATE);
                }
                else
                {
                    User32.SetWindowPos(hWnd,
                                        User32.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 0, 0,
                                        User32.SetWindowPosFlags.SWP_NOMOVE | User32.SetWindowPosFlags.SWP_NOSIZE);
                }
            }

            if (_isLayeredChanged)
            {
                _isLayeredChanged = false;
                SetBitFlag(ref _extendedStyle, (int)User32.SetWindowLongFlags.WS_EX_LAYERED, isLayered);
                User32.SetWindowLong(hWnd, User32.WindowLongIndexFlags.GWL_EXSTYLE, (User32.SetWindowLongFlags)_extendedStyle);
            }

            if (_crKeyChanged || _bAlphaChanged || _dwFlagsChanged)
            {
                _crKeyChanged = false;
                _bAlphaChanged = false;
                _dwFlagsChanged = false;
                NativeMethods.SetLayeredWindowAttributes(hWnd, crKey, bAlpha, dwFlags);
            }
        }


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
            return string.Format($"hWnd={hWnd}, title={title}, isVisible={isVisible}, crKey={crKey}, bAlpha={bAlpha}, dwFlags={dwFlags}, isLayered={isLayered}");
        }
    }
}
