using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.ViewModel;
using System;

namespace Stealth.ViewModel.Tests
{
    [TestClass]
    public class WindowInfoItemModelTests
    {
        [TestMethod]
        public void HWnd_PreservesPointerSizedValue()
        {
            if (IntPtr.Size != 8)
            {
                Assert.Inconclusive("This test requires a 64-bit process.");
            }

            var expected = new IntPtr(0x123456789L);
            var item = new WindowInfoItemModel { HWnd = expected };

            Assert.AreEqual(expected, item.HWnd);
        }

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
