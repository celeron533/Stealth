using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Model.Tests
{
    [TestClass()]
    public class UpdateServiceTests
    {
        [TestMethod()]
        public void CheckForUpdateTest()
        {
            UpdateService update = new UpdateService();
            var info = update.CheckForUpdate().Result;
            Assert.IsNotNull(info);
        }

        [TestMethod()]
        public void UpdateToLatestReleaseTest()
        {
            UpdateService update = new UpdateService();
            var info = update.UpdateToLatestRelease().Result;
            Assert.IsNotNull(info);
        }
    }
}