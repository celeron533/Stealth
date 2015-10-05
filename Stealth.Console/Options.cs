using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealth.Console
{
    public class Options
    {
        [Option('f', "filter", Required = false,
            HelpText = "Search filter on window name. Works only when \"hwnd\" is not provided.")]
        public string filter { get; set; }

        #region window operations
        [Option('w', "hwnd", Required = false,
            HelpText = "hWnd of the target window.")]
        public int hWnd { get; set; }

        [Option('a', "balpha", Required = false, DefaultValue = 255,
            HelpText = "bAlpha (0-255) of the target window.")]
        public int bAlpha { get; set; }

        [Option('t',"topmost",Required =false,
            HelpText ="Set the window pin to top.")]
        public bool TopMost { get; set; }

        [Option('r', "reset", Required = false, DefaultValue = false,
            HelpText = "Reset target window's status.")]
        public bool isReset { get; set; }
        #endregion

        [HelpOption]
        public string GetUsage()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            var helpText = new HelpText()
            {
                AdditionalNewLineAfterOption = false,
                AddDashesToOption = true
            };
            helpText.AddOptions(this);
            helpText.AddPostOptionsLine("If there is no argument provided, list all windows.");
            return helpText;
        }
    }
}
