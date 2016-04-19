using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Web;
using System.Web.Mvc;
using EC2_Management_Tool.Models;

namespace EC2_Management_Tool.Controllers
{
    public class S3Controller : Controller
    {
        // GET: S3
        public ActionResult S3()
        {
            dynamic model = new ExpandoObject();
            S3Model s3m = new S3Model();
            EC2ServersModels ec2m = new EC2ServersModels();
            var s3vols = s3m.getVolumes();
            var freevols = s3m.getFreeVolumes();
            var instances = ec2m.getInstances();
            model.volumes = s3vols;
            model.freevolumes = freevols;
            model.instances = instances;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateVolume(int size, string type, string zone)
        {
            S3Model s3m = new S3Model();
            s3m.CreateVolume(size, type, zone);
            return RedirectToAction("S3");
        }

        [HttpPost]
        public ActionResult DeleteVolume(string volumeId)
        {
            S3Model s3m = new S3Model();
            s3m.DeleteVolume(volumeId);
            return RedirectToAction("S3");
        }

        [HttpPost]
        public ActionResult AttachVolume(string volumeId, string instanceId, string device)
        {
            S3Model s3m = new S3Model();
            s3m.AttachVolume(volumeId, instanceId, device);
            return RedirectToAction("S3");
        }
    }
}