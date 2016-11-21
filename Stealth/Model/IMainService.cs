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
        ObservableCollection<WindowInfoItem> GetWindowData();
        void RefreshWindowData();
        void ResetWindow(WindowInfoItem item);
        void FilterByTitle(string titleText);
    }
}
