using Stealth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Stealth.Design
{
    public class MainService : IMainService
    {
        ObservableCollection<WindowInfoItem> windowInfoItemList;

        public MainService()
        {
            windowInfoItemList = new ObservableCollection<WindowInfoItem>();
        }

        public ObservableCollection<WindowInfoItem> GetWindowData()
        {
            windowInfoItemList.Clear();
            DummyDataSource();
            return windowInfoItemList;
        }

        public void RefreshWindowData()
        {
            //do nothing.
        }

        private void DummyDataSource()
        {
            windowInfoItemList.Add(new WindowInfoItem() { hWnd = 100, title = "Stealth Design", opacity = 127 });
            windowInfoItemList.Add(new WindowInfoItem() { hWnd = 899646, title = "Stealth Design", opacity = 127 });
        }
    }
}
