using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DebuggingDemo2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()

        {
            int firstVal = 10;
            int secondVal = 5;
            int result = firstVal / secondVal;
            ViewBag.Message = "Witamy w mvc";
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}