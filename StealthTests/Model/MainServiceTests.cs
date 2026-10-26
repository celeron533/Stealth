using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
