using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.WPF
{
    public class MainWindowViewModel
    {
        public MainWindowModel model { get; set; }

        public DelegateCommand setWindow { get; set; }
        public DelegateCommand refreshWindowList { get; set; }

        public MainWindowViewModel()
        {
            model = new MainWindowModel();

            setWindow = new DelegateCommand();
            refreshWindowList = new DelegateCommand();

            setWindow.ExecuteCommand = new Action<object>(model.SetWindow);
            refreshWindowList.ExecuteCommand = new Action<object>(model.RefreshList);
        }
    }
}
