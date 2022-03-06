using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            if(userid != null)
            {
                var getdetail = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                var getrole = getdetail.Roles.Where(x => x.UserId == userid).FirstOrDefault();
                var selectrole = db.Roles.Where(x => x.Id == getrole.RoleId).FirstOrDefault();
                if (selectrole.Name == "Admin")
                {
                    return RedirectToAction("UserAccounts", "Admin");
                }
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}