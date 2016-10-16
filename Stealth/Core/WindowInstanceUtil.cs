using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PInvoke;
using System.Diagnostics;

namespace Stealth.Core
{
    public class WindowInstanceUtil
    {
        public List<WindowInstanceInfo> ListAllWindows()
        {
            List<WindowInstanceInfo> hWndList = new List<WindowInstanceInfo>();
            User32.EnumWindows((IntPtr hwnd, IntPtr lParam) =>
                                {
                                    if (User32.IsWindow(hwnd))
                                    {
                                        WindowInstanceInfo wii = new WindowInstanceInfo(hwnd);
                                        // normally only get the visible window's detailed info
                                        if (wii.isVisible)
                                        {
                                            wii.GetDetailedInfo();
                                            Debug.WriteLine(wii.ToString());
                                            hWndList.Add(wii);
                                        }
                                        else
                                            wii = null;
                                    }
                                    return true;    //always return true
                                },
                                IntPtr.Zero);
            return hWndList;
        }

    }
}
