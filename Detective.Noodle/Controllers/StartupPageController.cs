using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Detective.Noodle.Controllers
{
    public class StartupPageController : Controller
    {
        // GET: StartupPage
        public ActionResult Index()
        {
            return View();
        }
    }
}