using DemoManageSys.Areas.Filters;
using Microsoft.AspNetCore.Mvc;
using Model;
using DemoManageSys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoManageSys.Areas.Admin.Controllers
{
    [Area("Admin")]
    [MyFilter]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}