using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth
{
    public class WindowInstanceDetail: WindowInstanceInfo
    {
        public bool NotObsolete { set; get; }
        public bool IsModified { set; get; }
    }
}
