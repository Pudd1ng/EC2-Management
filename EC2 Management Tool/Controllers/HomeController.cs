using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC2_Management_Tool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "EC2 Management and Scheduling";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Student details";

            return View();
        }

        public ActionResult CreateInstance()
        {
            return null;
        }
    }
}