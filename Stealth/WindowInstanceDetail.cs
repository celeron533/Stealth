using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth
{
    public class WindowInstanceDetail: WindowInstanceInfo
    {
        public bool IsAlive { set; get; }  //if the windows is not destoried
        public bool IsModified { set; get; }
        
        public int Opacity { set; get; }
    }
}
