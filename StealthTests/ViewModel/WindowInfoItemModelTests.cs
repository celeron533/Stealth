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
        public void TitleAndRemoval_NotifyChanges()
        {
            var item = new WindowInfoItemModel();
            var titleChanged = false;
            var removalChanged = false;
            item.PropertyChanged += (sender, args) =>
            {
                titleChanged |= args.PropertyName == nameof(item.Title);
                removalChanged |= args.PropertyName == nameof(item.IsRemoved);
            };

            item.Title = "Example";
            item.IsRemoved = true;

            Assert.IsTrue(titleChanged);
            Assert.IsTrue(removalChanged);
        }
    }
}
