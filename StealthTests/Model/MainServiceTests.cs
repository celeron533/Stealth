using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model.Tests
{
    [TestClass()]
    public class MainServiceTests
    {
        [TestMethod()]
        public void GetWindowDataTest()
        {
            var result = new ObservableCollection<WindowInfoItemModel>();
            MainService mainService = new MainService();
            mainService.GetWindowData();
            if (mainService.windowInfoViewList.Count > 2)
                mainService.windowInfoViewList.RemoveAt(1);
            mainService.GetWindowData();
        }
    }
}