﻿using Stealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Design
{
    public class DesignAboutService : IAboutService
    {
        public string GetVersionString()
        {
            return "1.0.0.0";
        }
    }
}
