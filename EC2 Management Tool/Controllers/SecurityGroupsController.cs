using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EC2_Management_Tool.Controllers
{
    public class SecurityGroupsController : Controller
    {
        // GET: SecurityGroup
        public ActionResult SecurityGroups()
        {
            return View();
        }
        
        public ActionResult CreateGroup(string groupName, string groupDescription)
        {
            return View();
        }
    }
}