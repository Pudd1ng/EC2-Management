using System;
using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using System.Web;
using System.Web.Mvc;
using EC2_Management_Tool.Models;

namespace EC2_Management_Tool.Controllers
{
    public class SecurityGroupsController : Controller
    {
        // GET: SecurityGroup
        public ActionResult SecurityGroups()
        {
            dynamic model = new ExpandoObject();
            SecurityGroupModel sgm = new SecurityGroupModel();
            var awsGroups = sgm.GetGroups();
            model.group = awsGroups;
            return View(model);
        }
        
        public ActionResult CreateGroup(string groupName, string groupDescription)
        {
            return View();
        }
    }
}