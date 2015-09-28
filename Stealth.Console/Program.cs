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
                if (options.hWnd==0)
                {
                    WindowInstanceService windowInstanceService = new WindowInstanceService();
                    var list = windowInstanceService.GetWindowInstanceInfoDetailList()
                        .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle));
                    foreach (var item in list)
                    {
                        System.Console.WriteLine(item.ToString());
                    }
                }
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
                System.Console.WriteLine(options.GetUsage());
            }


            //////////////
            //while (false)
            //{
            //    System.Console.WriteLine("=====");
            //    WindowInstanceService windowInstanceService = new WindowInstanceService();
            //    var list = windowInstanceService.GetWindowInstanceInfoDetailList()
            //        .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle));
            //    foreach (var item in list)
            //    {
            //        if (item.windowTitle== "无标题 - 记事本")
            //        {
            //            item.isLayered = !item.isLayered;
            //            item.transparencyProperty.dwFlags = (uint)User32.LWA.LWA_ALPHA;
            //            item.transparencyProperty.bAlpha = 200;
            //        }
            //        System.Console.WriteLine(item.ToString());
            //    }
            //    System.Console.ReadLine();
            //}
        }
    }
}
