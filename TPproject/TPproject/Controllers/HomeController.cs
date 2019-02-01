using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPproject.Models;

namespace TPproject.Controllers
{
    public class HomeController : Controller
    {
        WaybillContext orderContext = new WaybillContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}