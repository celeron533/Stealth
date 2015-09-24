using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.Core.WindowInstance
{
    public class WindowInstanceInfoDetail : WindowInstanceInfoBase
    {
        public WindowInstanceInfoDetail(IntPtr hWnd) : base(hWnd)
        { }

        public bool IsAlive { set; get; }  //if the windows is not destoried
        public bool IsModified { set; get; }
        public bool IsTransparency { set; get; }
        public bool IsOnTop { set; get; }

        public int Transparency { set; get; }

        public override string ToString()
        {
            return hWnd + ", " + windowTitle + ", " + isWindowVisible;
        }


    }
}
