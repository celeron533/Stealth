using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.ViewModel;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Stealth.Model.Tests
{
    [TestClass]
    public class MainServiceTests
    {
        private static MainService CreateServiceWithItems(params WindowInfoItemModel[] items)
        {
            var service = (MainService)FormatterServices.GetUninitializedObject(typeof(MainService));
            service.windowInfoViewList = new ObservableCollection<WindowInfoItemModel>(items);
            return service;
        }

        [TestMethod]
        public void FilterByTitle_UsesCaseInsensitiveMatching()
        {
            var visibleMatch = new WindowInfoItemModel { Title = "Notepad", IsTitleFilteredVisible = true, IsIncludeEmptyTitleVisible = true, IsIncludeRemovedVisible = true };
            var visibleMismatch = new WindowInfoItemModel { Title = "Calculator", IsTitleFilteredVisible = true, IsIncludeEmptyTitleVisible = true, IsIncludeRemovedVisible = true };
            var nullTitle = new WindowInfoItemModel { Title = null, IsTitleFilteredVisible = true, IsIncludeEmptyTitleVisible = true, IsIncludeRemovedVisible = true };
            var service = CreateServiceWithItems(visibleMatch, visibleMismatch, nullTitle);

            service.FilterByTitle("NOTE");

            Assert.IsTrue(visibleMatch.IsVisible);
            Assert.IsFalse(visibleMismatch.IsVisible);
            Assert.IsFalse(nullTitle.IsVisible);
        }

        [TestMethod]
        public void FilterByIncludeEmptyTitle_TogglesVisibilityForEmptyItems()
        {
            var emptyTitle = new WindowInfoItemModel { Title = string.Empty, IsTitleFilteredVisible = true, IsIncludeEmptyTitleVisible = true, IsIncludeRemovedVisible = true };
            var normalTitle = new WindowInfoItemModel { Title = "Editor", IsTitleFilteredVisible = true, IsIncludeEmptyTitleVisible = true, IsIncludeRemovedVisible = true };
            var service = CreateServiceWithItems(emptyTitle, normalTitle);

            service.FilterByIncludeEmptyTitle(false);

            Assert.IsFalse(emptyTitle.IsVisible);
            Assert.IsTrue(normalTitle.IsVisible);

            service.FilterByIncludeEmptyTitle(true);

            Assert.IsTrue(emptyTitle.IsVisible);
            Assert.IsTrue(normalTitle.IsVisible);
        }

        [TestMethod]
        public void FilterByIncludeRemoved_TogglesVisibilityForRemovedItems()
        {
            var removedItem = new WindowInfoItemModel { Title = "Removed", IsRemoved = true, IsTitleFilteredVisible = true, IsIncludeEmptyTitleVisible = true, IsIncludeRemovedVisible = true };
            var activeItem = new WindowInfoItemModel { Title = "Active", IsRemoved = false, IsTitleFilteredVisible = true, IsIncludeEmptyTitleVisible = true, IsIncludeRemovedVisible = true };
            var service = CreateServiceWithItems(removedItem, activeItem);

            service.FilterByIncludeRemoved(false);

            Assert.IsFalse(removedItem.IsVisible);
            Assert.IsTrue(activeItem.IsVisible);

            service.FilterByIncludeRemoved(true);

            Assert.IsTrue(removedItem.IsVisible);
            Assert.IsTrue(activeItem.IsVisible);
        }
    }
}
