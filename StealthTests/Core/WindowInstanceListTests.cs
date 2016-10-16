using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stealth.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stealth.Core.Tests
{
    [TestClass()]
    public class WindowInstanceListTests
    {
        [TestMethod()]
        public void ListAllWindowsTest()
        {
            WindowInstanceUtil wil = new WindowInstanceUtil();
            var expected = wil.ListAllWindows();
            CollectionAssert.AllItemsAreNotNull(expected);
        }
    }
}