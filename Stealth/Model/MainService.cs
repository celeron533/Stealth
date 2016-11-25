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

        public MainService()
        {
            util = new WindowInstanceUtil();
            windowInfoViewList = new ObservableCollection<WindowInfoItemModel>();
            RefreshWindowData();
        }


        public ObservableCollection<WindowInfoItemModel> GetWindowData()
        {
            UpdateWindowInfoViewList(windowInfoViewList, windowsInstanceList);
            return windowInfoViewList;
        }


        private void UpdateWindowInfoViewList(ObservableCollection<WindowInfoItemModel> targetViewList,
                                                List<WindowInstanceInfo> sourceNativeList)
        {
            // first, tag all current target item status as 'removed'
            foreach (var targetViewListItem in targetViewList)
            {
                targetViewListItem.isRemoved = true;
            }

            // then using the source to match target items one by one
            foreach (var windowInsatnceItem in sourceNativeList)
            {
                var matchedTargetItem = targetViewList.SingleOrDefault(item => item.hWnd == windowInsatnceItem.hWnd.ToInt32());
                if (matchedTargetItem == null)    // new (matchedTargetItem is created from default value)
                {
                    matchedTargetItem = new WindowInfoItemModel();
                    targetViewList.Add(matchedTargetItem);

                }
                matchedTargetItem.CopyFrom(windowInsatnceItem);
                matchedTargetItem.isRemoved = false;
            }
        }


        public void RefreshWindowData()
        {
            //please note that the UI (ListBoxItem) content is not refershed if updating nested elements
            windowsInstanceList = util.RetrieveAllWindows(true);
            UpdateWindowInfoViewList(windowInfoViewList, windowsInstanceList);
        }


        public void ResetWindow(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Reset window: {item.hWnd}, {item.title}"));
        }


        public void ChangeOpacity(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Change opacity: {item.title}, {item.opacity}"));
            var nativeWindow = windowsInstanceList.SingleOrDefault(window => window.hWnd == (IntPtr)item.hWnd);
            if (nativeWindow != null)
            {
                nativeWindow.isLayered = true;
                nativeWindow.bAlpha = (byte)item.opacity;
                nativeWindow.dwFlags = (int)MyUser32.LWA.LWA_ALPHA;
                nativeWindow.CommitChanges();
            }
        }


        public void SetTopMost(WindowInfoItemModel item)
        {
            Console.WriteLine(string.Format($"Set TopMost: {item.title}, {item.isTopMost}"));
            var nativeWindow = windowsInstanceList.SingleOrDefault(window => window.hWnd == (IntPtr)item.hWnd);
            if (nativeWindow != null)
            {
                nativeWindow.isTopMost = item.isTopMost;
                nativeWindow.CommitChanges();
            }
        }


        public void FilterByTitle(string titleText)
        {
            if (string.IsNullOrWhiteSpace(titleText)) //disable filter
            {
                foreach (var item in windowInfoViewList)
                {
                    item.isFilteredVisible = true;
                }
            }
            else
            {
                string titleText_Lower = titleText.ToLower();
                foreach (var item in windowInfoViewList)
                {
                    if (item.title.ToLower().Contains(titleText_Lower))
                        item.isFilteredVisible = true;
                    else
                        item.isFilteredVisible = false;
                }
            }

        }

    }
}
