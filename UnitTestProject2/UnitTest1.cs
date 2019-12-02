using System;
//using Real_Time_Commenting.Controllers;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CS4125_Kek_Hotel_Management.Controllers;
namespace test
{
    [TestClass]
    public class UnitTestProject2
    {
        [TestMethod]
        public void IndexCheck()
        {
            HomeController HomeController = new HomeController();
            ViewResult result = HomeController.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void AboutCheck()
        {
            HomeController HomeController = new HomeController();
            ViewResult result = HomeController.About() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void ContactCheck()
        {
            HomeController HomeController = new HomeController();
            ViewResult result = HomeController.Contact() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

    }
}