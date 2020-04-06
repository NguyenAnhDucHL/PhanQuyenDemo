using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [Route("home/about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("home/contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [Route("NoPermission")]
        public ActionResult NoPermission()
        {
            return View();
        }
    }
}