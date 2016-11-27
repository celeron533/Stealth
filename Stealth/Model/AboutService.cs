using Stealth.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model
{
    public class AboutService : IAboutService
    {
        public AboutService()
        {
        }

        public string GetVersionString()
        {
            return Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
