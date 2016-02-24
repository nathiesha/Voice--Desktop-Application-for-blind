using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voice2;
using System.Windows.Forms;


namespace EasyCoTest
{
    [TestClass]
    public class UnitTestEmail
    {

        [TestMethod]
        public void TestMethod1()
        {
            Email fun = new Email();
            String next = "back";
            String actual = fun.clickMe(next);
            Assert.AreEqual(next, actual);
        }


        [TestMethod]
        public void TestMethod2()
        {
            Email fun = new Email();
            String next = "close";
            String actual = fun.clickMe(next);
            Assert.AreEqual(next, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Email fun = new Email();
            String next = "Log In";
            String actual = fun.clickMe(next);
            Assert.AreEqual(next, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Email fun = new Email();
            String next = "Add new User";
            String actual = fun.clickMe(next);
            Assert.AreEqual(next, actual);
        }
    }
}
