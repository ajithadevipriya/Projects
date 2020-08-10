using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WooliesTest.Services;
using WooliesTest.Services.External.Entity;

namespace WooliesTest.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public ActionResult Index()
        {
            return View();
        }
    }
}
