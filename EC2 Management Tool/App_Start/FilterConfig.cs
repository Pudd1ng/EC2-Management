﻿using System.Web;
using System.Web.Mvc;

namespace EC2_Management_Tool
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
