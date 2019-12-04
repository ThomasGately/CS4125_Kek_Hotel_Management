using System;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using CS4125_Kek_Hotel_Management;
using CS4125_Kek_Hotel_Management.Controllers;
using System.Web.Mvc;

namespace UnitTestProject4
{
    [TestClass]
    public class TestHomrContoller
    {
        [TestMethod]
        public void Index()
        {
            HomeController HomeController = new HomeController();
            ViewResult result = HomeController.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void About()
        {
            HomeController HomeController = new HomeController();
            ViewResult result = HomeController.About() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void Contact()
        {
            HomeController HomeController = new HomeController();
            ViewResult result = HomeController.Contact() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(string.Empty, result.ViewName);
        }

    }
}