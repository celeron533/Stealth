using Stealth.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.Core.WindowInstance
{
    public class WindowInstanceInfoBase
    {
        public WindowInstanceInfoBase(IntPtr hWnd)
        {
            this.hWnd = hWnd;
        }

        private IntPtr _hWnd;
        public IntPtr hWnd
        {
            private set { _hWnd = value; }
            get { return _hWnd; }
        }

        public string windowTitle
        {
            get
            {
                StringBuilder strbTitle = new StringBuilder(255);
                User32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                return strbTitle.ToString();
            }
        }

        public bool isWindowVisible
        {
            get
            {
                return User32.IsWindowVisible(hWnd);
            }
        }
    }
}
