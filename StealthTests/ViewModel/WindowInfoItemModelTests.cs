using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.ViewModel;

namespace Stealth.ViewModel.Tests
{
    [TestClass]
    public class WindowInfoItemModelTests
    {
        [TestMethod]
        public void UpdateVisibility_HidesEmptyTitleWhenConfigured()
        {
            var item = new WindowInfoItemModel
            {
                Title = string.Empty,
                IsRemoved = false,
                IsTitleFilteredVisible = true,
                IsIncludeEmptyTitleVisible = false,
                IsIncludeRemovedVisible = true
            };

            Assert.IsFalse(item.IsVisible);
        }

        [TestMethod]
        public void UpdateVisibility_HidesRemovedWindowWhenExcluded()
        {
            var item = new WindowInfoItemModel
            {
                Title = "Example",
                IsRemoved = true,
                IsTitleFilteredVisible = true,
                IsIncludeEmptyTitleVisible = true,
                IsIncludeRemovedVisible = false
            };

            Assert.IsFalse(item.IsVisible);
        }

        [TestMethod]
        public void UpdateVisibility_ShowsWindowOnlyWhenAllConditionsPass()
        {
            var item = new WindowInfoItemModel
            {
                Title = "Example",
                IsRemoved = false,
                IsTitleFilteredVisible = true,
                IsIncludeEmptyTitleVisible = true,
                IsIncludeRemovedVisible = true
            };

            Assert.IsTrue(item.IsVisible);
        }

        [TestMethod]
        public void UpdateVisibility_RecomputesAfterMutableStateChanges()
        {
            var item = new WindowInfoItemModel
            {
                Title = "Example",
                IsRemoved = false,
                IsTitleFilteredVisible = true,
                IsIncludeEmptyTitleVisible = true,
                IsIncludeRemovedVisible = true
            };

            item.Title = string.Empty;
            item.IsIncludeEmptyTitleVisible = false;
            Assert.IsFalse(item.IsVisible);

            item.Title = "Example";
            item.IsRemoved = true;
            item.IsIncludeRemovedVisible = true;
            Assert.IsTrue(item.IsVisible);
        }
    }
}
