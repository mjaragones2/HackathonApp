using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserAccount()
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();


            return View();
        }
    }
}