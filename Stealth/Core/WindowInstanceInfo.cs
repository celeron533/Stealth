using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using System.Diagnostics;
using System.Drawing;

namespace Stealth.Core
{
    public class WindowInstanceInfo
    {
        unsafe private static readonly HWND HWND_TOPMOST = new HWND((void*)(-1));
        unsafe private static readonly HWND HWND_NOTOPMOST = new HWND((void*)(-2));
        unsafe private static readonly HWND HWND_TOP = new HWND((void*)(0));
        unsafe private static readonly HWND HWND_BOTTOM = new HWND((void*)(1));


        #region Basic Info

        /// <summary>
        /// Window hWnd
        /// </summary>
        internal readonly HWND HWnd;

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
        internal WINDOWINFO WindowInfo;
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

        public Bitmap iconBitmap
        {
            get;
            private set;
        }

        #endregion

        internal WindowInstanceInfo(HWND hWnd) : base()
        {
            this.HWnd = hWnd;
            GetBasicInfo();
        }

        /// <summary>
        /// Get basic info from window
        /// </summary>
        public void GetBasicInfo()
        {
            IsVisible = PInvoke.IsWindowVisible(HWnd);
            char[] t = new char[256];
            Span<char> span = new Span<char>(t);
            // PInvoke.GetWindowText(hWnd); may have some exceptions when accessing system processes
            PInvoke.GetWindowText(HWnd, span);
            Title = span.Slice(0, span.IndexOf('\0')).ToString();
            PInvoke.GetWindowInfo(HWnd, ref WindowInfo);
        }

        /// <summary>
        /// Get more details from a window
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
            _extendedStyle = PInvoke.GetWindowLong(HWnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
            IsLayered = (_extendedStyle & (int)WINDOW_EX_STYLE.WS_EX_LAYERED) != 0;
            IsTopMost = (_extendedStyle & (int)WINDOW_EX_STYLE.WS_EX_TOPMOST) != 0;

            try
            {
                uint processId = 0;
                NativeMethods.GetWindowThreadProcessId(HWnd, out processId);
                if (processId > 0)
                process = Process.GetProcessById((int)processId);
            }
            catch(ArgumentException)
            {
                // normally caused by process not running
            }
            catch(Exception)
            {
                // other unknown exceptions
            }

            GetIcon();
        }


        /// <summary>
        /// Get the icon from process
        /// </summary>
        public void GetIcon()
        {
            if (process == null)
            {
                //iconBitmap = Bitmap.FromHicon(SystemIcons.WinLogo.Handle);
                return;
            }
            // https://stackoverflow.com/a/23978207/2075611
            try
            {
                iconBitmap = Icon.ExtractAssociatedIcon(process.MainModule.FileName).ToBitmap();
            }
            catch (Exception ex)
            {
                // expected errors if there is no icon or the process is 64-bit
                if (ex is ArgumentException || ex is System.ComponentModel.Win32Exception)
                {
                    iconBitmap = Bitmap.FromHicon(SystemIcons.Application.Handle);
                }
                else
                {
                    //iconBitmap = Bitmap.FromHicon(SystemIcons.Error.Handle);
                }
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
                    PInvoke.SetWindowPos(HWnd,
                                        HWND_TOPMOST, 0, 0, 0, 0,
                                        SET_WINDOW_POS_FLAGS.SWP_NOMOVE | SET_WINDOW_POS_FLAGS.SWP_NOSIZE | SET_WINDOW_POS_FLAGS.SWP_NOACTIVATE);
                }
                else
                {
                    PInvoke.SetWindowPos(HWnd,
                                        HWND_NOTOPMOST, 0, 0, 0, 0,
                                        SET_WINDOW_POS_FLAGS.SWP_NOMOVE | SET_WINDOW_POS_FLAGS.SWP_NOSIZE);
                }
            }

            if (_isLayeredChanged)
            {
                _isLayeredChanged = false;
                SetBitFlag(ref _extendedStyle, (int)WINDOW_EX_STYLE.WS_EX_LAYERED, IsLayered);
                PInvoke.SetWindowLong(HWnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, _extendedStyle);
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
