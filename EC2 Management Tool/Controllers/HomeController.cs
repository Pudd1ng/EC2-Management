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
            SecurityGroupModel sgm = new SecurityGroupModel();
            var groups = sgm.GetGroups();
            var instances = ec2m.getInstances();
            model.instances = instances;
            model.groups = groups;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateInstance(string amiId, string keyPairName, string instanceType, int minCount, int maxCount, string group)
        {
            EC2ServersModels ec2m = new EC2ServersModels();
            ec2m.CreateInstance(amiId, keyPairName, instanceType, minCount, maxCount, group);
            return RedirectToAction("Index");
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