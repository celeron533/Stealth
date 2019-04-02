using GalaSoft.MvvmLight.Command;
using Stealth.Core;
using Stealth.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model
{
    public class MainService : IMainService
    {
        // window info from native user32
        private WindowInstanceUtil util;
        private List<WindowInstanceInfo> windowsInstanceList;

        // window info for view
        public ObservableCollection<WindowInfoItemModel> windowInfoViewList { get; set; }

        // filters
        private string titleText;
        private bool includeEmptyTitle, includeRemoved;

        public MainService()
        {
            util = new WindowInstanceUtil();
            windowInfoViewList = new ObservableCollection<WindowInfoItemModel>();
            RefreshWindowData();
        }


        public ObservableCollection<WindowInfoItemModel> GetWindowListData()
        {
            UpdateWindowInfoItemModelList(windowInfoViewList, windowsInstanceList);
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
                var matchedTargetItem = targetModelList.SingleOrDefault(item => item.HWnd == windowInsatnceItem.HWnd.ToInt32());
                if (matchedTargetItem == null)    // new (matchedTargetItem is created from default value)
                {
                    matchedTargetItem = new WindowInfoItemModel();
                    targetModelList.Add(matchedTargetItem);

                }
                matchedTargetItem.CopyFrom(windowInsatnceItem);
                
                matchedTargetItem.IsRemoved = false;
            }
        }


        public void RefreshWindowData()
        {
            //please note that the UI (ListBoxItem) content is not refershed if updating nested elements
            windowsInstanceList = util.RetrieveAllWindows(true);
            UpdateWindowInfoItemModelList(windowInfoViewList, windowsInstanceList);
            FilterByTitle(this.titleText);
            FilterByIncludeEmptyTitle(this.includeEmptyTitle);
            FilterByIncludeRemoved(this.includeRemoved);
        }


        public void Detail(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Reset window: {item.HWnd}, {item.Title}"));
        }


        public void ChangeOpacity(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Change opacity: {item.Title}, {item.Opacity}"));
            var nativeWindow = windowsInstanceList.SingleOrDefault(window => window.HWnd == (IntPtr)item.HWnd);
            if (nativeWindow != null)
            {
                nativeWindow.IsLayered = true;
                nativeWindow.BAlpha = (byte)item.Opacity;
                nativeWindow.DwFlags = (int)NativeMethods.LWA.LWA_ALPHA;
                nativeWindow.CommitChanges();
            }
        }


        public void SetTopMost(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Set TopMost: {item.Title}, {item.IsTopMost}"));
            var nativeWindow = windowsInstanceList.SingleOrDefault(window => window.HWnd == (IntPtr)item.HWnd);
            if (nativeWindow != null)
            {
                nativeWindow.IsTopMost = item.IsTopMost;
                nativeWindow.CommitChanges();
            }
        }


        public void FilterByTitle(string titleText)
        {
            this.titleText = titleText;
            if (string.IsNullOrWhiteSpace(this.titleText)) //disable filter
            {
                foreach (var item in windowInfoViewList)
                {
                    item.IsTitleFilteredVisible = true;
                }
            }
            else
            {
                string titleText_Lower = this.titleText.ToLower();
                foreach (var item in windowInfoViewList)
                {
                    if (item.Title.ToLower().Contains(titleText_Lower))
                        item.IsTitleFilteredVisible = true;
                    else
                        item.IsTitleFilteredVisible = false;
                }
            }
        }

        public void FilterByIncludeEmptyTitle(bool? includeEmptyTitle)
        {
            this.includeEmptyTitle = (bool)includeEmptyTitle;
            foreach (var item in windowInfoViewList)
            {
                item.IsIncludeEmptyTitleVisible = this.includeEmptyTitle;
            }
        }

        public void FilterByIncludeRemoved(bool? includeRemoved)
        {
            this.includeRemoved = (bool)includeRemoved;
            foreach (var item in windowInfoViewList)
            {
                item.IsIncludeRemovedVisible = this.includeRemoved;
            }
        }
    }
}
