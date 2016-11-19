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
        public List<WindowInstanceInfo> RetrieveAllWindows(bool getDetailedInfo = false)
        {
            List<WindowInstanceInfo> hWndList = new List<WindowInstanceInfo>();
            User32.EnumWindows((IntPtr hwnd, IntPtr lParam) =>
                                {
                                    if (User32.IsWindow(hwnd))
                                    {
                                        // get basic info from targe window
                                        WindowInstanceInfo wii = new WindowInstanceInfo(hwnd);
                                        // normally only get the visible window's detailed info
                                        if (wii.isVisible)
                                        {
                                            if (getDetailedInfo)
                                            {
                                                wii.GetDetailedInfo();
                                            }
                                            Debug.WriteLine(wii.ToString());
                                            hWndList.Add(wii);
                                        }
                                        else
                                            wii = null;
                                    }
                                    return true;    //this native method requires always return true
                                },
                                IntPtr.Zero);
            return hWndList;
        }

    }
}
