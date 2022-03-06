using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Constants = HackathonApp.Models.Constants;

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

        public ActionResult UserAccounts()
        {
            var db = new ApplicationDbContext();
            List<RegisterViewModel> model = new List<RegisterViewModel>();
            var getusers = db.Users.Where(x => x.Email != Constants.Adminemail).ToList();
            
            if(getusers.Count > 0)
            {
                foreach(var user in getusers)
                {
                    model.Add(new RegisterViewModel { Address = user.Address, Bdate = user.Bdate, Contact = user.PhoneNumber, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName });
                }
            }

            return View();
        }
    }
}