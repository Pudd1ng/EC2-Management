using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC2_Management_Tool.Models;
using System.Dynamic;

namespace EC2_Management_Tool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            dynamic model = new ExpandoObject();
            EC2ServersModels ec2m = new EC2ServersModels();
            var instances = ec2m.getInstances();
            model.instances = instances;
            return View(model);
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

        public ActionResult S3()
        {
            return View();
        }

        public ActionResult SecurityGroups()
        {
            return View();
        }

        public ActionResult CreateInstance()
        {
            return null;
        }
    }
}