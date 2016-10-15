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
        public bool isLayered;
        //public struct LAYERED
        //{
            public uint crKey;
            public byte bAlpha;
            public uint dwFlags;
        //}
        #endregion

        public WindowInstanceInfo(IntPtr hWnd) : base()
        {
            this.hWnd = hWnd;
            GetBasicInfo();
        }

        public void GetBasicInfo()
        {
            isVisible = User32.IsWindowVisible(hWnd);
            char[] t = new char[255];
            //User32.GetWindowText(hWnd); may have some exceptions.
            User32.GetWindowText(hWnd, t, t.Length + 1);
            title = 
                t[0] == '\0' ? string.Empty : new string(t).Trim('\0');
            User32.GetWindowInfo(hWnd, ref windowInfo);
            Debug.WriteLine(this.ToString());
        }

        public void GetDetailedInfo()
        {
            //todo: topmost, transparancy
            //windowInfo.dwExStyle: WS_EX_LAYERED 0x00080000 before you start working on alpha
            //transparency  https://msdn.microsoft.com/en-us/library/windows/desktop/ms632599(v=vs.85).aspx#layered

            MyUser32.GetLayeredWindowAttributes(hWnd, out crKey, out bAlpha, out dwFlags);
        }

        public override string ToString()
        {
            return string.Format($"hWnd={hWnd}, title={title}, isVisible={isVisible}");
        }
    }
}
