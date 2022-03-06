using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
            HomeViewModel model = new HomeViewModel();
            List<FundViewModel> viewModels = new List<FundViewModel>();
            List<SupportingDocument> supportingDocuments = new List<SupportingDocument>();
            var listoffunds = db.Funds.Take(4).ToList();
            var listofdocs = db.Documents.ToList();
            if(listofdocs.Count > 0 )
            {
                foreach(var doc in listofdocs)
                {
                    supportingDocuments.Add(new SupportingDocument { FileType = doc.Filetype, Path = doc.Path, Id = doc.Id, Fundid = doc.Fundid, Userid = userid });
                }
            }
            if(listoffunds.Count > 0)
            {
                foreach(var fund in listoffunds)
                {
                    var getreact = db.Like.Where(x => x.Userid == userid && x.Fundid == fund.Id).FirstOrDefault();
                    var getonepic = db.Documents.Where(x => x.Fundid == fund.Id && x.Filetype == "Image").FirstOrDefault();
                    if(getonepic != null)
                    {
                        if(getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false });
                        }
                        
                    }
                    else
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false });
                        }
                    }
                    
                }
            }
            model.Documents = supportingDocuments;
            model.FundViews = viewModels;
            return View(model);
        }

        public FileResult Download(string fileName)
        {
            string myfile = fileName.Replace("~/Documents/", "");
            string fullPath = Path.Combine(Server.MapPath("../Documents"), myfile);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, MediaTypeNames.Application.Octet, fileName);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(HomeViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            Session["fundid"] = model.Id;
            Session["messa"] = model.Message;
            Session["amount"] = model.AmountGiven;
            return RedirectToAction("PaymentWithPaypal", "Paypal", new { AmountGiven = model.AmountGiven, Fundid = model.Id, Message = model.Message });
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddFund(HomeViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                return RedirectToAction("PaymentWithPaypal", "Paypal", new { AmountGiven = model.AmountGiven, Fundid = model.Fundid, Message = model.Message });

            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(FundViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();

            if(model.CommentMessage != null)
            {
                var comm = new FundComments
                {
                    Comment = model.CommentMessage,
                    DateCreated = DateTime.Now,
                    Fundid = model.Id,
                    Userid = userid
                };
                db.Comments.Add(comm);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ViewMoreFunds()
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            HomeViewModel model = new HomeViewModel();
            List<FundViewModel> viewModels = new List<FundViewModel>();
            List<SupportingDocument> supportingDocuments = new List<SupportingDocument>();
            var listoffunds = db.Funds.Take(4).ToList();
            var listofdocs = db.Documents.ToList();
            if (listofdocs.Count > 0)
            {
                foreach (var doc in listofdocs)
                {
                    supportingDocuments.Add(new SupportingDocument { FileType = doc.Filetype, Path = doc.Path, Id = doc.Id, Fundid = doc.Fundid, Userid = userid });
                }
            }
            if (listoffunds.Count > 0)
            {
                foreach (var fund in listoffunds)
                {
                    var getreact = db.Like.Where(x => x.Userid == userid && x.Fundid == fund.Id).FirstOrDefault();
                    var getonepic = db.Documents.Where(x => x.Fundid == fund.Id && x.Filetype == "Image").FirstOrDefault();
                    if (getonepic != null)
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false });
                        }

                    }
                    else
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false });
                        }
                    }

                }
            }
            model.Documents = supportingDocuments;
            model.FundViews = viewModels;
            return View(model);
        }

        [Authorize]
        public ActionResult IReact(int? id)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            if(id > 0)
            {
                var checkreact = db.Like.Where(x => x.Userid == userid && x.Fundid == id).FirstOrDefault();
                if(checkreact == null || checkreact.IsLiked == false)
                {
                    var react = new LikeReact
                    {
                        Fundid = (int)id,
                        IsLiked = true,
                        Userid = userid
                    };
                    db.Like.Add(react);
                    db.SaveChanges();
                }
                else
                {
                    checkreact.IsLiked = false;
                    db.Entry(checkreact).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        public ActionResult FAQ()
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
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            if (userid != null)
            {
                var getdetail = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                var getrole = getdetail.Roles.Where(x => x.UserId == userid).FirstOrDefault();
                var selectrole = db.Roles.Where(x => x.Id == getrole.RoleId).FirstOrDefault();
                if (selectrole.Name == "Admin")
                {
                    return RedirectToAction("UserAccounts", "Admin");
                }
            }
            HomeViewModel model = new HomeViewModel();
            List<FundViewModel> viewModels = new List<FundViewModel>();
            List<SupportingDocument> supportingDocuments = new List<SupportingDocument>();
            var listoffunds = db.Funds.Take(4).ToList();
            var listofdocs = db.Documents.ToList();
            if (listofdocs.Count > 0)
            {
                foreach (var doc in listofdocs)
                {
                    supportingDocuments.Add(new SupportingDocument { FileType = doc.Filetype, Path = doc.Path, Id = doc.Id, Fundid = doc.Fundid, Userid = userid });
                }
            }
            if (listoffunds.Count > 0)
            {
                foreach (var fund in listoffunds)
                {
                    var getreact = db.Like.Where(x => x.Userid == userid && x.Fundid == fund.Id).FirstOrDefault();
                    var getonepic = db.Documents.Where(x => x.Fundid == fund.Id && x.Filetype == "Image").FirstOrDefault();
                    if (getonepic != null)
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false });
                        }

                    }
                    else
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false });
                        }
                    }

                }
            }
            model.Documents = supportingDocuments;
            model.FundViews = viewModels;
            return View(model);
        }

        public ActionResult Works()
        {
            return View();
        }
        
    }
}