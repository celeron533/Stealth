using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth
{
    public class WindowInstanceDetail: WindowInstanceBase
    {
        public bool IsAlive { set; get; }  //if the windows is not destoried
        public bool IsModified { set; get; }
        public bool IsTransparency { set; get; }
        public int Transparency { set; get; }
        public int PinOnTop { set; get; }
    }
}
