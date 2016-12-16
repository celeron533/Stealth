using Stealth.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model
{
    public interface IMainService
    {
        ObservableCollection<WindowInfoItemModel> GetWindowListData();
        void RefreshWindowData();
        void Detail(WindowInfoItemModel item);
        void FilterByTitle(string titleText);
        void ChangeOpacity(WindowInfoItemModel item);
        void SetTopMost(WindowInfoItemModel item);
    }
}
