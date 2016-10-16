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
        public IntPtr hWnd;
        public string title;
        public bool isVisible;
        public User32.WINDOWINFO windowInfo;
        #endregion

        #region Detailed Info
        private int _extendedStyle;
        public bool isLayered;
        public uint crKey;
        public byte bAlpha; //uint
        public uint dwFlags;
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
            // User32.GetWindowText(hWnd); may have some exceptions.
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
            //todo: topmost
            //windowInfo.dwExStyle: WS_EX_LAYERED 0x00080000 before you start working on alpha
            //transparency  https://msdn.microsoft.com/en-us/library/windows/desktop/ms632599(v=vs.85).aspx#layered

            // Get transparency
            MyUser32.GetLayeredWindowAttributes(hWnd, out crKey, out bAlpha, out dwFlags);
            // Get IsLayered. Opacity works when IsLayered = true
            _extendedStyle = User32.GetWindowLong(hWnd, User32.WindowLongIndexFlags.GWL_EXSTYLE);
            isLayered = (_extendedStyle & (int)MyUser32.WS_EX.WS_EX_LAYERED) == (int)MyUser32.WS_EX.WS_EX_LAYERED;
        }

        /// <summary>
        /// Commit changes to the window
        /// </summary>
        public void CommitDetailedInfo()
        {
            if (isLayered)
                //   style  1011[0]11
                // layered  0000[1]00
                //      OR  1011[1]11
                _extendedStyle |= (int)MyUser32.WS_EX.WS_EX_LAYERED;
            else
                //   style  1011[1]11
                //~layered  1111[0]11
                //     AND  1011[0]11
                _extendedStyle &= ~(int)MyUser32.WS_EX.WS_EX_LAYERED;

            MyUser32.SetLayeredWindowAttributes(hWnd, crKey, bAlpha, dwFlags);
        }




        public override string ToString()
        {
            return string.Format($"hWnd={hWnd}, title={title}, isVisible={isVisible}, crKey={crKey}, bAlpha={bAlpha}, dwFlags={dwFlags}, isLayered={isLayered}");
        }
    }
}
