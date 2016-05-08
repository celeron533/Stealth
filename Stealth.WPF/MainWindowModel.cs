using Stealth.Core.Utilities;
using Stealth.Core.WindowInstance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Stealth.WPF
{
    public class MainWindowModel : NotificationObject
    {
        private ObservableCollection<WindowListModel> _windowListModel = new ObservableCollection<WindowListModel>();
        public ObservableCollection<WindowListModel> windowListModel
        {
            get { return _windowListModel; }
            set
            {
                if (_windowListModel != value)
                {
                    _windowListModel = value;
                    OnPropertyChanged("windowListModel");
                }
            }
        }

        private string _textFilter = "todo";
        public string textFilter
        {
            get { return _textFilter; }
            set
            {
                if (_textFilter != value)
                {
                    _textFilter = value;
                    OnPropertyChanged("textFilter");
                }
            }
        }



        private List<WindowInstanceInfoDetail> windowList = new List<WindowInstanceInfoDetail>();

        public void RefreshList(object obj)
        {
            windowList = new WindowInstanceService().GetWindowInstanceInfoDetailList()
                .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle)).ToList();
            //map to view model
            windowListModel.Clear();
            foreach (var w in windowList)
            {
                var window = new WindowListModel();
                window.hWnd = w.hWnd.ToInt32();
                window.windowTitle = w.windowTitle;
                window.alpha = w.transparencyProperty.dwFlags == (uint)User32.LWA.LWA_ALPHA ?
                    w.transparencyProperty.bAlpha : 255;

                windowListModel.Add(window);
            }
        }

        /// <summary>
        /// Set the Window Properties
        /// </summary>
        /// <param name="obj">hWnd</param>
        public void SetWindow(object obj)
        {
            var windowView = windowListModel.First(w => w.hWnd == (int)obj);
            var targetWindow = windowList.Find(w => w.hWnd.ToInt32() == (int)obj);
            if (targetWindow != null)
            {
                //targetWindow.isTopMost = checkBox_Top.Checked;
                targetWindow.isLayered = true;
                targetWindow.transparencyProperty.bAlpha = (byte)windowView.alpha;
                targetWindow.transparencyProperty.dwFlags = (uint)User32.LWA.LWA_ALPHA;
                //targetWindow.isModified = true;
            }
        }

    }
}
