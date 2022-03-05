using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonApp.Controllers
{
    [Authorize]
    public class FundController : Controller
    {
        // GET: Fund

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateFundProject()
        {

            return View();
        }

        [HttpPost]
        public ActionResult CreateFundProject(FundViewModel model)
        {
            var db = new ApplicationDbContext();
            var getuser = User.Identity.GetUserId();
            if(ModelState.IsValid)
            {
                Fund fund = new Fund { AmountNeeded = model.AmountNeeded, AmountAcquired = 0, DateCreated = DateTime.Now, DateEnd = model.DateEnd, DateUpdated = DateTime.Now, Story = model.Story, Title = model.Title, Userid = getuser };
                db.Funds.Add(fund);
                db.SaveChanges();

                foreach (HttpPostedFileBase file in model.File)
                {
                    if (file != null)
                    {
                        string name = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        name = name + extension;
                        var path = Path.Combine(Server.MapPath("~/Documents/"), name);
                        file.SaveAs(path);

                        var docs = new FundDocument { Created_at = DateTime.Now, Fundid = fund.Id, Path = name, UserId = getuser };
                        db.SaveChanges();
                    }
                }
                
            }
            return View();
        }
        
    }
}