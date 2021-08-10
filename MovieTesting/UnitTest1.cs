using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieHub;
using System;
using System.Data;

namespace MovieTesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            Database database = new Database();
            Assert.AreEqual(database.GetConnectionState(), ConnectionState.Open);
        }

        [TestMethod]
        public void Test2()
        {
            Database database = new Database();
            database.CloseConnection();
            Assert.AreEqual(database.GetConnectionState(), ConnectionState.Closed);
        }
    }
}
