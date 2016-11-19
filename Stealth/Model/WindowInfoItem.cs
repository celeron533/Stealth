using Stealth.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model
{
    public class WindowInfoItem
    {
        public int hWnd { get; set; }
        public string title { get; set; }
        public int opacity { get; set; }
        public bool isModified { get; set; }
        public bool isRemoved { get; set; }

        public void CopyFrom(WindowInstanceInfo nativeSource)
        {
            hWnd = nativeSource.hWnd.ToInt32();
            title = nativeSource.title;
            opacity = nativeSource.bAlpha;
        }

        public override string ToString()
        {
            return string.Format($"hWnd={hWnd}, title={title}, opacity={opacity}");
        }
    }
}
