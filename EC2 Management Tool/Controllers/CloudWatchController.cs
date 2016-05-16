using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC2_Management_Tool.Models;
using System.Dynamic;

namespace EC2_Management_Tool.Controllers
{
    public class CloudWatchController : Controller
    {
        // GET: CloudWatch
        public ActionResult CloudWatch()
        {
            dynamic model = new ExpandoObject();
            EC2ServersModels ec2m = new EC2ServersModels();
            var instances = ec2m.getInstances();
            model.instances = instances;
            return View(model);
        }

        [HttpPost]
        public ActionResult EnableMonitoring(string instanceId)
        {
            CloudWatchModel cwm = new CloudWatchModel();
            cwm.EnableMonitoring(instanceId);
            return RedirectToAction("CloudWatch");
        }
    }
}