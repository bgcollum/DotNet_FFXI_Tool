using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.Collections.Generic;
using DataAccessLayerInterfaces;
using DataAccessLayerFakes;
using DataAccessLayer;
using DataObjects;

namespace DataAccessLayerTests
{
    [TestClass]
    public class StatAccessorTests
    {
        StatListAccessor stat_list_accessor = null;

        [TestInitialize]
        public void TestSetup()
        {
            stat_list_accessor = new StatListAccessor();
        }
        [TestMethod]
        public void TestMethod1()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void Test_MSL()
        {
            List<string> testList = stat_list_accessor.RetrieveMasterStatList();
            Assert.AreEqual("Dicks", testList[0]);
        }
    }
}
