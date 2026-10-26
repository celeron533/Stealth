using Stealth.Core;
using Stealth.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Stealth.Model
{
    public class MainService : IMainService, IDisposable
    {
        // window info from native user32
        private WindowInstanceUtil util;
        private List<WindowInstanceInfo> windowsInstanceList;

        // window info for view
        public ObservableCollection<WindowInfoItemModel> windowInfoViewList { get; private set; }

        public MainService()
        {
            util = new WindowInstanceUtil();
            windowInfoViewList = new ObservableCollection<WindowInfoItemModel>();
            RefreshWindowData();
        }


        public ObservableCollection<WindowInfoItemModel> GetWindowListData()
        {
            return windowInfoViewList;
        }


        /// <summary>
        /// Update the WindowInfoItemModel list from known WindowInstanceInfo(native) list.
        /// If a window is no longer exist in WindowInstanceInfo(native) list, 
        /// it will be marked as 'isRemoved' in WindowInfoItemModel list
        /// </summary>
        /// <param name="targetModelList"></param>
        /// <param name="sourceNativeList"></param>
        private void UpdateWindowInfoItemModelList(ObservableCollection<WindowInfoItemModel> targetModelList,
                                                List<WindowInstanceInfo> sourceNativeList)
        {
            // first, tag all current target item status as 'removed'
            foreach (var targetViewListItem in targetModelList)
            {
                targetViewListItem.IsRemoved = true;
            }

            // then using the source to match target items one by one
            foreach (var windowInsatnceItem in sourceNativeList)
            {
                var matchedTargetItem = targetModelList.SingleOrDefault(item => item.HWnd == (IntPtr)windowInsatnceItem.HWnd);
                if (matchedTargetItem == null)    // new (matchedTargetItem is created from default value)
                {
                    matchedTargetItem = new WindowInfoItemModel();
                    matchedTargetItem.PropertyChanged += WindowInfoItemModel_PropertyChanged;
                    targetModelList.Add(matchedTargetItem);

                }
                matchedTargetItem.CopyFrom(windowInsatnceItem);

                matchedTargetItem.IsRemoved = false;
            }
        }


        public void RefreshWindowData()
        {
            //please note that the UI (ListBoxItem) content is not refershed if updating nested elements
            DisposeWindowSnapshots();

            windowsInstanceList = util.RetrieveAllWindows(true);
            UpdateWindowInfoItemModelList(windowInfoViewList, windowsInstanceList);
        }

        private void DisposeWindowSnapshots()
        {
            if (windowsInstanceList == null)
            {
                return;
            }

            foreach (var window in windowsInstanceList)
            {
                window.Dispose();
            }

            windowsInstanceList = null;
        }

        public void Dispose()
        {
            foreach (var item in windowInfoViewList)
            {
                item.PropertyChanged -= WindowInfoItemModel_PropertyChanged;
            }

            DisposeWindowSnapshots();
        }


        public void Detail(WindowInfoItemModel item)
        {
            if (item == null)
            {
                return;
            }

            MessageBox.Show(
                $"Handle: 0x{item.HWnd.ToInt64():X}\n" +
                $"Title: {(string.IsNullOrWhiteSpace(item.Title) ? "(empty)" : item.Title)}\n" +
                $"Opacity: {item.Opacity}\n" +
                $"TopMost: {item.IsTopMost}",
                "Window Detail",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }


        public void ChangeOpacity(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Change opacity: {item.Title}, {item.Opacity}"));
            var nativeWindow = windowsInstanceList.SingleOrDefault(window => (IntPtr)window.HWnd == item.HWnd);
            if (nativeWindow != null)
            {
                var opacity = (byte)Math.Max(byte.MinValue, Math.Min(byte.MaxValue, item.Opacity));
                nativeWindow.TrySetOpacity(opacity, out var diagnostic);
                item.OpacityError = diagnostic;
            }
        }

        public void UpdateOpacityFromOverlay(IntPtr handle, byte opacity)
        {
            var item = windowInfoViewList.SingleOrDefault(window => window.HWnd == handle && !window.IsRemoved);
            item?.UpdateOpacityFromNative(opacity);
        }


        public void SetTopMost(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Set TopMost: {item.Title}, {item.IsTopMost}"));
            var nativeWindow = windowsInstanceList.SingleOrDefault(window => (IntPtr)window.HWnd == item.HWnd);
            if (nativeWindow != null)
            {
                nativeWindow.IsTopMost = item.IsTopMost;
                nativeWindow.CommitChanges();
            }
        }

        private void WindowInfoItemModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var item = sender as WindowInfoItemModel;
            if (item == null || item.IsUpdatingFromNative || item.IsRemoved)
            {
                return;
            }

            if (e.PropertyName == nameof(WindowInfoItemModel.Opacity))
            {
                ChangeOpacity(item);
            }
            else if (e.PropertyName == nameof(WindowInfoItemModel.IsTopMost))
            {
                SetTopMost(item);
            }
        }
    }
}
