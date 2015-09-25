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
            while (true)
            {
                System.Console.WriteLine("=====");
                WindowInstanceService windowInstanceService = new WindowInstanceService();
                var list = windowInstanceService.GetWindowInstanceInfoDetailList()
                    .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle));
                foreach (var item in list)
                {
                    System.Console.WriteLine(item.ToString());
                    System.Console.WriteLine("{0}, {1}, {2}", item.transparencyProperty.crKey, item.transparencyProperty.bAlpha, item.transparencyProperty.dwFlags);
                }
                System.Console.ReadLine();
            }
        }
    }
}
