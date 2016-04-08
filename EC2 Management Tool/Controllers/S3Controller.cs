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
            var s3vols = s3m.getVolumes();
            model.volumes = s3vols;
            return View(model);
        }
    }
}