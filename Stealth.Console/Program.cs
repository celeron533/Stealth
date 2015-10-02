using Stealth.Core.Utilities;
using Stealth.Core.WindowInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Options options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                //list all windows
                if (options.hWnd==0)
                {
                    WindowInstanceService windowInstanceService = new WindowInstanceService();
                    var list = windowInstanceService.GetWindowInstanceInfoDetailList()
                        .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle));
                    if (!string.IsNullOrEmpty(options.filter))
                    {
                        list = list.Where(c => c.windowTitle.ToLower().Contains(options.filter.ToLower()));
                    }
                    foreach (var item in list)
                    {
                        System.Console.WriteLine(item.ToString());
                    }
                }
                //work on target window
                else
                {
                    var window = new WindowInstanceInfoDetail((IntPtr)options.hWnd);
                    if (options.isReset)
                    {
                        window.transparencyProperty.bAlpha = 255;
                        window.transparencyProperty.dwFlags = (uint)User32.LWA.LWA_UNDEFINED;
                    }
                    else
                    {
                        window.isLayered = true;
                        window.transparencyProperty.bAlpha = (byte)options.bAlpha;
                        window.transparencyProperty.dwFlags = (uint)User32.LWA.LWA_ALPHA;
                    }
                }
            }
            else
            {
                // Display the default usage information
                //System.Console.WriteLine(options.GetUsage());
            }
        }
    }
}
