using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voice2;
using System.Windows.Forms;


namespace EasyCoTest


{
    [TestClass]
    public class UnitTestFunctions
    {
        [TestMethod]
        public void TestMethod1()

        {
            Functions fun = new Functions();
            String next = "email";
            String actual = fun.clicker(next);
            Assert.AreEqual(next, actual);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Functions fun = new Functions();
            String next = "skype";
            String actual = fun.clicker(next);
            Assert.AreEqual(next, actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Functions fun = new Functions();
            String next = "back";
            String actual = fun.clicker(next);
            Assert.AreEqual(next, actual);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Functions fun = new Functions();
            String next = "close";
            String actual = fun.clicker(next);
            Assert.AreEqual(next, actual);
        }

        [TestMethod]
        public void TestMethod5()
        {
            Functions fun = new Functions();
            String actual = fun.Key();
            Assert.AreEqual("yes", actual);
        }

       
    }
}
