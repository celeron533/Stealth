using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.Console
{
    public class Options
    {
        [Option('h', "hwnd", Required=false, HelpText = "hWnd of the target window.")]
        public int hWnd { get; set; }

        [Option('a', "balpha", Required = false, DefaultValue = 255, HelpText = "bAlpha (0-255) of the target window.")]
        public int bAlpha { get; set; }

        [Option('r', "reset", Required = false, DefaultValue = false, HelpText = "Reset target window.")]
        public bool isReset { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            // this without using CommandLine.Text
            //  or using HelpText.AutoBuild
            //var usage = new StringBuilder();
            //usage.AppendLine("Quickstart Application 1.0");
            //usage.AppendLine("Read user manual for usage instructions...");
            //return usage.ToString();
            return HelpText.AutoBuild(this);
        }
    }
}
