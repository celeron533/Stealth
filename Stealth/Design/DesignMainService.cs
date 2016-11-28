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

        public ObservableCollection<WindowInfoItemModel> GetWindowData()
        {
            windowInfoItemList.Clear();
            DummyDataSource();
            return windowInfoItemList;
        }

        public void RefreshWindowData() { }

        public void Detail(WindowInfoItemModel item) { }

        public void FilterByTitle(string titleText) { }

        public void ChangeOpacity(WindowInfoItemModel item) { }

        public void SetTopMost(WindowInfoItemModel item) { }

        private void DummyDataSource()
        {
            windowInfoItemList.Add(new WindowInfoItemModel() { hWnd = 100, title = "Stealth Design", opacity = 127 });
            windowInfoItemList.Add(new WindowInfoItemModel() { hWnd = 899646, title = "Stealth Design", opacity = 127 });
        }
    }
}
