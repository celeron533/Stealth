using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvoke;
using System.Diagnostics;

namespace Stealth.Core
{
    public class WindowInstanceList
    {
        private List<WindowInstanceInfo> hWnds = new List<WindowInstanceInfo>();

        public List<WindowInstanceInfo> ListAllWindows()
        {
            hWnds.Clear();
            User32.EnumWindows((IntPtr hwnd, IntPtr lParam) =>
                                {
                                    if (User32.IsWindow(hwnd))
                                    {
                                        hWnds.Add(new WindowInstanceInfo(hwnd));
                                    }
                                    return true;    //always return true
                                },
                                IntPtr.Zero);
            return hWnds;
        }

    }
}
