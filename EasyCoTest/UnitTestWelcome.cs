using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voice2;
using System.Windows.Forms;


namespace EasyCoTest
{
    [TestClass]
    public class UnitTestWelcome
    {
        [TestMethod]
        public void TestMethod1()
        {
            welcome wel = new welcome();
           int actual= wel.MyTest(1,2);
           Assert.AreEqual(3, actual);
        }


        [TestMethod]
        public void TestMethod2()
        {
            welcome wel = new welcome();
            String actual = wel.welcomeSpeeech();
            Assert.AreEqual("yes", actual);
        }

        [TestMethod]
        public void TestMethod3()
        {
            welcome wel = new welcome();
            String actual = wel.clicker();
            Assert.AreEqual("yes", actual);
        }


    }
}
