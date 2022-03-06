using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Constants = HackathonApp.Models.Constants;

namespace HackathonApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //List of Users
        public ActionResult UserAccounts()
        {
            var db = new ApplicationDbContext();
            List<RegisterViewModel> model = new List<RegisterViewModel>();
            var getusers = db.Users.Where(x => x.Email != Constants.Adminemail).ToList();
            
            if(getusers.Count > 0)
            {
                foreach(var user in getusers)
                {
                    model.Add(new RegisterViewModel { Address = user.Address, Bdate = user.Bdate, Contact = user.PhoneNumber, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Created_at = user.DateCreated, Updated_at = user.DateUpdated, Userid = user.Id, IsLocked = user.IsDelete });
                }
            }

            return View(model);
        }

        //Show user detail
        public ActionResult UserDetail(string id)
        {
            var db = new ApplicationDbContext();
            UserViewModel model = new UserViewModel();
            if (!String.IsNullOrEmpty(id))
            {
                var getdetail = db.Users.Where(x => x.Id == id).FirstOrDefault();
                if(getdetail != null)
                {
                    model.Userid = getdetail.Id;
                    model.Address = getdetail.Address;
                    model.Bdate = getdetail.Bdate;
                    model.Contact = getdetail.PhoneNumber;
                    model.Created_at = getdetail.DateCreated;
                    model.Updated_at = getdetail.DateUpdated;
                    model.FirstName = getdetail.FirstName;
                    model.LastName = getdetail.LastName;
                    model.ProfilePic = getdetail.Image;
                    model.Email = getdetail.Email;
                    model.IsLocked = getdetail.IsDelete;
                }
            }
            return View(model);
        }

        //Deactivate Account
        public ActionResult Deactivate(string id)
        {
            var db = new ApplicationDbContext();
            if(!String.IsNullOrEmpty(id))
            {
                var getdetail = db.Users.Where(x => x.Id == id).FirstOrDefault();
                getdetail.IsDelete = true;
                db.Entry(getdetail).State = EntityState.Modified;
                db.SaveChanges();
            }    
            return RedirectToAction("UserAccounts");
        }

        //Activate Account
        public ActionResult Activate(string id)
        {
            var db = new ApplicationDbContext();
            if (!String.IsNullOrEmpty(id))
            {
                var getdetail = db.Users.Where(x => x.Id == id).FirstOrDefault();
                getdetail.IsDelete = false;
                db.Entry(getdetail).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("UserAccounts");
        }

        [HttpPost]
        public ActionResult CreateCommission(ComisssionRate model)
        {
            var db = new ApplicationDbContext();
            model.DateCreated = DateTime.Now;
            db.Rate.Add(model);
            db.SaveChanges();
            return View();
        }

        public ActionResult DisplayFundTransactions()
        {
            var db = new ApplicationDbContext();
            List<AddFundViewModel> model = new List<AddFundViewModel>();
            var transactions = db.FundTransactions.ToList();
            if(transactions.Count > 0)
            {
                foreach(var tran in transactions)
                {
                    var benefUser = db.Users.Where(x => x.Id == tran.ReceiverId).FirstOrDefault();
                    var funder = db.Users.Where(x => x.Id == tran.Userid).FirstOrDefault();
                    model.Add(new AddFundViewModel
                    {
                        AmountGiven = tran.AmountGiven,
                        BenefName = benefUser.FirstName + " " + benefUser.LastName,
                        Message = tran.Message,
                        Fundid = tran.Fundid,
                        FunderName = funder.FirstName + " " + funder.LastName,
                        FundTransId = tran.Id,
                        Userid = funder.Id,
                        DateCreated = tran.DateCreated
                    });
                }
            }
            return View(model);
        }


    }
}