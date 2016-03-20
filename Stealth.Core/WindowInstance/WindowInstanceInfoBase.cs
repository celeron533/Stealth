using Stealth.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.Core.WindowInstance
{
    /// <summary>
    /// All members are read-only.
    /// </summary>
    public class WindowInstanceInfoBase
    {
        public WindowInstanceInfoBase(IntPtr hWnd)
        {
            this._hWnd = hWnd;
            refreshAll();
        }

        public void refreshAll()
        {
            StringBuilder strbTitle = new StringBuilder(255);
            User32.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
            this._windiwsTitle = strbTitle.ToString();
            this._isWindowVisible = User32.IsWindowVisible(hWnd);
        }

        private IntPtr _hWnd;
        public IntPtr hWnd
        {
            get { return _hWnd; }
        }

        private string _windiwsTitle;
        public string windowTitle
        {
            get { return _windiwsTitle; }
            set { _windiwsTitle = value; }
        }

        private bool _isWindowVisible;
        public bool isWindowVisible
        {
            get { return _isWindowVisible; }
        }

        public override string ToString()
        {
            return string.Format("hWnd={0}, Title=\"{1}\", Visible={2}", hWnd, windowTitle, isWindowVisible);
        }
    }
}
