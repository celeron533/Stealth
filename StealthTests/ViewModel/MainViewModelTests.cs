using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Stealth.ViewModel.Tests
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void WindowsView_FiltersTitleCaseInsensitively()
        {
            var service = new FakeMainService(
                new WindowInfoItemModel { Title = "Notepad" },
                new WindowInfoItemModel { Title = "Calculator" },
                new WindowInfoItemModel { Title = null });
            var viewModel = new MainViewModel(service);

            viewModel.TitleFilterText = "NOTE";

            Assert.AreEqual("Notepad", viewModel.WindowsView.Cast<WindowInfoItemModel>().Single().Title);
        }

        [TestMethod]
        public void WindowsView_TogglesEmptyAndRemovedItems()
        {
            var service = new FakeMainService(
                new WindowInfoItemModel { Title = "Active" },
                new WindowInfoItemModel { Title = "Closed", IsRemoved = true },
                new WindowInfoItemModel { Title = string.Empty });
            var viewModel = new MainViewModel(service);

            Assert.AreEqual(1, viewModel.WindowsView.Cast<WindowInfoItemModel>().Count());
            viewModel.includeRemoved = true;
            viewModel.includeEmptyTitle = true;
            Assert.AreEqual(3, viewModel.WindowsView.Cast<WindowInfoItemModel>().Count());
            viewModel.includeRemoved = false;
            Assert.AreEqual(2, viewModel.WindowsView.Cast<WindowInfoItemModel>().Count());
        }

        [TestMethod]
        public void RefreshCommand_RefreshesViewWithoutReplacingCollection()
        {
            var item = new WindowInfoItemModel { Title = "Active" };
            var service = new FakeMainService(item);
            var viewModel = new MainViewModel(service);
            var originalCollection = viewModel.windowsInfoItemList;
            service.OnRefresh = () => item.IsRemoved = true;

            viewModel.RefreshCommand.Execute(null);

            Assert.AreSame(originalCollection, viewModel.windowsInfoItemList);
            Assert.AreEqual(0, viewModel.WindowsView.Cast<WindowInfoItemModel>().Count());
        }

        [TestMethod]
        public void UpdateOpacityFromOverlay_ForwardsWindowHandleAndOpacity()
        {
            var service = new FakeMainService();
            var viewModel = new MainViewModel(service);
            var handle = new IntPtr(12345);

            viewModel.UpdateOpacityFromOverlay(handle, 120);

            Assert.AreEqual(handle, service.LastUpdatedHandle);
            Assert.AreEqual((byte)120, service.LastUpdatedOpacity);
        }

        private sealed class FakeMainService : IMainService
        {
            private readonly ObservableCollection<WindowInfoItemModel> _items;

            public FakeMainService(params WindowInfoItemModel[] items)
            {
                _items = new ObservableCollection<WindowInfoItemModel>(items);
            }

            public Action OnRefresh { get; set; }
            public IntPtr LastUpdatedHandle { get; private set; }
            public byte LastUpdatedOpacity { get; private set; }
            public ObservableCollection<WindowInfoItemModel> GetWindowListData() => _items;
            public void RefreshWindowData() => OnRefresh?.Invoke();
            public void Detail(WindowInfoItemModel item) { }
            public void ChangeOpacity(WindowInfoItemModel item) { }
            public void UpdateOpacityFromOverlay(IntPtr handle, byte opacity)
            {
                LastUpdatedHandle = handle;
                LastUpdatedOpacity = opacity;
            }
            public void SetTopMost(WindowInfoItemModel item) { }
        }
    }
}
