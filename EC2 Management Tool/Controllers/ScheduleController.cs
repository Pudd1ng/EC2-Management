using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC2_Management_Tool.Models;
using System.Dynamic;

namespace EC2_Management_Tool.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Schedule()
        {
            dynamic model = new ExpandoObject();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSchedule(string Name, string Days, string Hours)
        {
            ScheduleModel sm = new ScheduleModel();
            sm.createSchedule(Name, Days, Hours);
            return RedirectToAction("Schedule");
        }

        [HttpPost]
        public ActionResult DeleteSchedule(string Name)
        {
            ScheduleModel sm = new ScheduleModel();
            sm.deleteSchedule(Name);
            return RedirectToAction("Schedule");
        }

        [HttpPost]
        public ActionResult AssignSchedule(string scheduleName, string instanceId)
        {
            ScheduleModel sm = new ScheduleModel();
            sm.assignSchedule(instanceId, scheduleName);
            return RedirectToAction("Schedule");
        }
    }
}