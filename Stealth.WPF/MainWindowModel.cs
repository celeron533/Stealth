using Stealth.Core.WindowInstance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.WPF
{
    public class MainWindowModel : NotificationObject
    {

        #region windowList
        private List<WindowInstanceInfoDetail> _windowList = new List<WindowInstanceInfoDetail>();

        public List<WindowInstanceInfoDetail> windowList
        {
            get { return _windowList; }
            set
            {
                if (_windowList != value)
                {
                    _windowList = value;
                    OnPropertyChanged("windowList");
                }
            }
        }
        #endregion

        public void RefreshList(object obj)
        {
            windowList = new WindowInstanceService().GetWindowInstanceInfoDetailList()
                .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle)).ToList();
        }


        #region Test
        private string _test = "";

        public string test
        {
            get
            {
                return _test;
            }
            set
            {
                if (_test != value)
                {
                    _test = value;
                    OnPropertyChanged("test");
                }
            }
        }

        public void DoTest(object obj)
        {
            this.test += "!";
        }

        #endregion
    }
}
