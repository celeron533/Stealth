using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.WPF
{
    public class MainWindowViewModel
    {
        public MainWindowModel model { get; set; }

        public DelegateCommand cmd { get; set; }
        public DelegateCommand refreshWindowList { get; set; }
        

        public MainWindowViewModel()
        {
            model = new MainWindowModel();

            cmd = new DelegateCommand();
            cmd.ExecuteCommand = new Action<object>(model.SetWindow);

            refreshWindowList = new DelegateCommand();
            refreshWindowList.ExecuteCommand = new Action<object>(model.RefreshList);
        }
    }
}
