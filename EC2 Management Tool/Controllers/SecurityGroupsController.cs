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
        
        [HttpPost]
        public ActionResult CreateGroup(string groupName, string groupDescription, string ipRange, string protocol, int fromPort, int toPort)
        {
            SecurityGroupModel sgm = new SecurityGroupModel();
            sgm.CreateGroup(groupName, groupDescription, ipRange, protocol, fromPort, toPort);
            return RedirectToAction("SecurityGroups");
        }

        [HttpPost]
        public ActionResult DeleteGroup(string groupName)
        {
            SecurityGroupModel sgm = new SecurityGroupModel();
            sgm.DeleteGroup(groupName);
            return RedirectToAction("SecurityGroups");
        }
    }
}