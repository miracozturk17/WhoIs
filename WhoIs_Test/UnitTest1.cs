using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DG_WhoIs_Project;
namespace DG_WhoIs_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string querrydomain = "aselsan.com.tr";
            WhoIs_Project.QuerryDoamin(querrydomain);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string querrydomain = "aselsan.com.fr";
            WhoIs_Project.QuerryDoamin(querrydomain);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string querrydomain = "twitter.com";
            WhoIs_Project.QuerryDoamin(querrydomain);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string querrydomain = "twitter.fr";
            WhoIs_Project.QuerryDoamin(querrydomain);
        }
    }
}
