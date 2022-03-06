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
            List<FundViewModel> viewModels = new List<FundViewModel>();
            var listoffunds = db.Funds.ToList();
            if(listoffunds.Count > 0)
            {
                foreach(var fund in listoffunds)
                {
                    var imagefile = new[] { ".png", ".jpg", ".jpeg", ".jiff", ".gif" };
                    var getonepic = db.Documents.Where(x => x.Fundid == fund.Id && imagefile.Contains(x.Path)).FirstOrDefault();
                    if(getonepic != null)
                    {
                        viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path });
                    }
                    else
                    {
                        viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value });
                    }
                    
                }
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(CommentViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();



            return RedirectToAction("Index");
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