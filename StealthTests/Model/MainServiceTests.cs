using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.ViewModel;
using System;
using System.Reflection;

namespace Stealth.Model.Tests
{
    [TestClass]
    public class MainServiceTests
    {
        [TestMethod]
        public void RefreshWindowData_KeepsTheSameCollection()
        {
            using (var service = new MainService())
            {
                var items = service.GetWindowListData();

                service.RefreshWindowData();

                Assert.AreSame(items, service.GetWindowListData());
            }
        }

        [TestMethod]
        public void Dispose_ReleasesCurrentWindowSnapshots()
        {
            var service = new MainService();
            var snapshots = typeof(MainService).GetField("windowsInstanceList", BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.IsNotNull(snapshots.GetValue(service));

            service.Dispose();
            service.Dispose();

            Assert.IsNull(snapshots.GetValue(service));
        }

        [TestMethod]
        public void UpdateOpacityFromOverlay_UpdatesOnlyMatchingActiveItemWithoutWriteBack()
        {
            using (var service = new MainService())
            {
                var matching = new WindowInfoItemModel { HWnd = new IntPtr(-12345), Title = "Active", Opacity = 255, OpacityError = "old error" };
                var unrelated = new WindowInfoItemModel { HWnd = new IntPtr(-12346), Title = "Other", Opacity = 255 };
                var removed = new WindowInfoItemModel { HWnd = new IntPtr(-12347), Title = "Removed", IsRemoved = true, Opacity = 255 };
                var isUpdating = typeof(WindowInfoItemModel).GetProperty("IsUpdatingFromNative", BindingFlags.Instance | BindingFlags.NonPublic);
                var guardedDuringNotification = false;
                matching.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(matching.Opacity))
                    {
                        guardedDuringNotification = (bool)isUpdating.GetValue(matching);
                    }
                };
                service.GetWindowListData().Add(matching);
                service.GetWindowListData().Add(unrelated);
                service.GetWindowListData().Add(removed);

                service.UpdateOpacityFromOverlay(matching.HWnd, 100);
                service.UpdateOpacityFromOverlay(removed.HWnd, 100);

                Assert.AreEqual(100, matching.Opacity);
                Assert.IsNull(matching.OpacityError);
                Assert.IsTrue(guardedDuringNotification);
                Assert.AreEqual(255, unrelated.Opacity);
                Assert.AreEqual(255, removed.Opacity);
            }
        }
    }
}
