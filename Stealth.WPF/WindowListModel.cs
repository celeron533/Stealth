using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stealth.WPF
{
    public class WindowListModel : NotificationObject
    {
        private int _hwnd;
        public int hWnd
        {
            get { return _hwnd; }
            set
            {
                if (_hwnd != value)
                {
                    _hwnd = value;
                    OnPropertyChanged("hWnd");
                }
            }
        }

        private string _windowTitle;
        public string windowTitle
        {
            get { return _windowTitle; }
            set
            {
                if (_windowTitle != value)
                {
                    _windowTitle = value;
                    OnPropertyChanged("windowTitle");
                }
            }
        }

        private int _alpha;
        public int alpha
        {
            get { return _alpha; }
            set
            {
                if (_alpha != value)
                {
                    _alpha = value;
                    OnPropertyChanged("alpha");
                }
            }
        }

    }
}
