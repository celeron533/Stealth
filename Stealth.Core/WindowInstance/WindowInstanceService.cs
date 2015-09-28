using Stealth.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.Core.WindowInstance
{
    public class WindowInstanceService
    {
        public List<WindowInstanceInfoDetail> GetWindowInstanceInfoDetailList()
        {
            List<WindowInstanceInfoDetail> WindowInstanceInfoResultList = new List<WindowInstanceInfoDetail>();

            //window traversal
            User32.EnumDesktopWindows(IntPtr.Zero
                , (IntPtr hWnd, int lParam) =>
                    {
                        WindowInstanceInfoResultList.Add(new WindowInstanceInfoDetail(hWnd));
                        return true;    //always return true
                    }
                , IntPtr.Zero);

            return WindowInstanceInfoResultList;
        }
    }
}
