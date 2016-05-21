using Stealth.Core.Utilities;
using Stealth.Core.WindowInstance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Stealth.WPF
{
    public class MainWindowModel : NotificationObject
    {
        private ObservableCollection<WindowListModel> _windowListModels = new ObservableCollection<WindowListModel>();
        public ObservableCollection<WindowListModel> windowListModels
        {
            get { return _windowListModels; }
            set
            {
                if (_windowListModels != value)
                {
                    _windowListModels = value;
                    OnPropertyChanged("windowListModel");
                }
            }
        }

        private string _textFilter = "";
        public string textFilter
        {
            get { return _textFilter; }
            set
            {
                if (_textFilter != value)
                {
                    _textFilter = value;
                    OnPropertyChanged("textFilter");
                    UpdateWindowListViewByFilter(_textFilter);
                }
            }
        }



        private List<WindowInstanceInfoDetail> windowList = new List<WindowInstanceInfoDetail>();

        /// <summary>
        /// Refresh the window list
        /// </summary>
        /// <param name="obj"></param>
        public void RefreshList(object obj)
        {
            windowList = new WindowInstanceService().GetWindowInstanceInfoDetailList()
                .Where(c => c.isWindowVisible && !string.IsNullOrEmpty(c.windowTitle)).ToList();
            //map to view model
            windowListModels.Clear();
            foreach (var w in windowList)
            {
                var window = new WindowListModel();
                window.hWnd = w.hWnd.ToInt32();
                window.windowTitle = w.windowTitle;
                window.alpha = w.transparencyProperty.dwFlags == (uint)User32.LWA.LWA_ALPHA ?
                    w.transparencyProperty.bAlpha : 255;
                window.isRowVisible = Visibility.Visible;

                windowListModels.Add(window);
            }
            //update by filter
            UpdateWindowListViewByFilter(textFilter);
        }

        /// <summary>
        /// Set the Window Properties
        /// </summary>
        /// <param name="obj">hWnd</param>
        public void SetWindow(object obj)
        {
            var windowView = windowListModels.First(w => w.hWnd == (int)obj);
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


        private void UpdateWindowListViewByFilter(string title)
        {
            if (windowListModels != null && windowListModels.Count > 0)
            {
                foreach (var window in windowListModels)
                {
                    if (string.IsNullOrWhiteSpace(title) ||
                        window.windowTitle.ToLower().Contains(title.ToLower()))
                    {
                        window.isRowVisible = Visibility.Visible;
                    }
                    else
                    {
                        window.isRowVisible = Visibility.Collapsed;
                    }
                }
            }
        }

    }
}
