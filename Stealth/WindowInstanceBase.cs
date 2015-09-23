using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth
{
    public class WindowInstanceBase
    {
        public IntPtr hWnd { set; get; }
        public string WindowTitle { set; get; }

        public override string ToString()
        {
            return hWnd + ", " + WindowTitle;
        }
    }
}
