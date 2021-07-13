using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Core.Caching;
using App.Core;
using App.Core.UseAge;

namespace Test.App.Core
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestIInitIoc()
        {
            TestClass.Cache();
            Export.Cache.SetWithMins("name", "Jack");
            Assert.IsTrue(Export.Cache.Get<string>("name").Equals("Jack"));
        }
    }
}
