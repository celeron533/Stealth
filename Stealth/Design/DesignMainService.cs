using Stealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Stealth.ViewModel;

namespace Stealth.Design
{
    public class DesignMainService : IMainService
    {
        ObservableCollection<WindowInfoItemModel> windowInfoItemList;

        public DesignMainService()
        {
            windowInfoItemList = new ObservableCollection<WindowInfoItemModel>();
        }

        public ObservableCollection<WindowInfoItemModel> GetWindowListData()
        {
            windowInfoItemList.Clear();
            DummyDataSource();
            return windowInfoItemList;
        }

        public void RefreshWindowData() { }

        public void Detail(WindowInfoItemModel item) { }

        public void FilterByTitle(string titleText) { }

        public void FilterByIncludeEmptyTitle(bool? isChecked) { }

        public void FilterByIncludeRemoved(bool? isChecked) { }

        public void ChangeOpacity(WindowInfoItemModel item) { }

        public void SetTopMost(WindowInfoItemModel item) { }

        private void DummyDataSource()
        {
            windowInfoItemList.Add(new WindowInfoItemModel() { HWnd = 100, Title = "Stealth Design", Opacity = 127 });
            windowInfoItemList.Add(new WindowInfoItemModel() { HWnd = 899646, Title = "Stealth Design", Opacity = 127 });
        }
    }
}
