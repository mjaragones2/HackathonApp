using HackathonApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

                foreach (HttpPostedFileBase file in model.ImageFile)
                {
                    if (file != null)
                    {
                        string name = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        name = name + extension;
                        var path = Path.Combine(Server.MapPath("~/Documents/"), name);
                        file.SaveAs(path);

                        var docs = new FundDocument { Created_at = DateTime.Now, Fundid = fund.Id, Path = name, UserId = getuser };
                        db.Documents.Add(docs);
                        db.SaveChanges();
                    }
                }
                
            }
            return RedirectToAction("ListOfFunds");
        }
        

        public ActionResult ListOfFunds()
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            var getfunds = db.Funds.Where(x => x.Userid == userid).ToList();
            return View(getfunds);
        }

        public ActionResult FundDetail(int? id)
        {
            var db = new ApplicationDbContext();
            FundViewModel viewModel = new FundViewModel();
            viewModel.Documents = new List<SupportingDocument>();
            if(id > 0)
            {
                var getfund = db.Funds.Where(x => x.Id == id).FirstOrDefault();
                if(getfund != null)
                {
                    viewModel.Id = getfund.Id;
                    viewModel.Title = getfund.Title;
                    viewModel.Story = getfund.Story;
                    viewModel.AmountNeeded = getfund.AmountNeeded.Value;
                    viewModel.AmountAcquired = getfund.AmountAcquired;
                    viewModel.DateCreated = getfund.DateCreated;
                    viewModel.DateEnd = getfund.DateEnd.Value;
                    viewModel.DateUpdated = getfund.DateUpdated;

                    var getdocs = db.Documents.Where(x => x.Fundid == getfund.Id).ToList();
                    if(getdocs.Count > 0)
                    {
                        foreach(var doc in getdocs)
                        {
                            viewModel.Documents.Add(new SupportingDocument { Id = doc.Id, Path = doc.Path });
                        }
                        
                    }
                }
            }
            return View(viewModel);
        }

        public ActionResult EditFundDetail(int? id)
        {
            var db = new ApplicationDbContext();
            FundViewModel viewModel = new FundViewModel();
            viewModel.Documents = new List<SupportingDocument>();
            if (id > 0)
            {
                var getfund = db.Funds.Where(x => x.Id == id).FirstOrDefault();
                if (getfund != null)
                {
                    viewModel.Id = getfund.Id;
                    viewModel.Title = getfund.Title;
                    viewModel.Story = getfund.Story;
                    viewModel.AmountNeeded = getfund.AmountNeeded.Value;
                    viewModel.AmountAcquired = getfund.AmountAcquired;
                    viewModel.DateCreated = getfund.DateCreated;
                    viewModel.DateEnd = getfund.DateEnd.Value;
                    viewModel.DateUpdated = getfund.DateUpdated;

                    var getdocs = db.Documents.Where(x => x.Fundid == getfund.Id).ToList();
                    if (getdocs.Count > 0)
                    {
                        foreach (var doc in getdocs)
                        {
                            viewModel.Documents.Add(new SupportingDocument { Id = doc.Id, Path = doc.Path });
                        }

                    }
                    Session["fundid"] = id;
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditFundDetail(FundViewModel model)
        {
            
            try
            {
                var db = new ApplicationDbContext();
                var getfund = db.Funds.Where(x => x.Id == model.Id).FirstOrDefault();
                if(getfund != null)
                {
                    if (!String.IsNullOrEmpty(model.Story))
                        getfund.Story = model.Story;
                    else
                        getfund.Story = getfund.Story;
                    if (!String.IsNullOrEmpty(model.Title))
                        getfund.Title = model.Title;
                    else
                        getfund.Title = getfund.Title;
                    if (model.AmountNeeded != 0)
                        getfund.AmountNeeded = model.AmountNeeded;
                    else
                        getfund.AmountNeeded = getfund.AmountNeeded;
                    if (model.DateEnd != null)
                        getfund.DateEnd = model.DateEnd;
                    else
                        getfund.DateEnd = getfund.DateEnd;

                    getfund.DateUpdated = DateTime.Now;
                    db.Entry(getfund).State = EntityState.Modified;
                    db.SaveChanges();
                    foreach (var file in model.ImageFile)
                    {
                        if(file != null)
                        {
                            string name = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            var imagees = new[] { ".png", ".gif", ".jpeg", ".jpg", ".jiff" };
                            var docfiles = new[] { ".ppt", ".pptx", ".docx", ".pdf" };
                            var videofiles = new[] { ".mkv", ".mp4", ".mov" };
                            var myfile = name + extension;
                            if(imagees.Contains(extension))
                            {
                                var path = Path.Combine(Server.MapPath("~/images/"), myfile);
                                file.SaveAs(path);
                                var fundoc = new FundDocument { Fundid = model.Id, Path = myfile, UserId = getfund.Userid, Created_at = DateTime.Now, Filetype = "Image" };
                                db.Documents.Add(fundoc);
                                db.SaveChanges();
                            }
                            else if(docfiles.Contains(extension))
                            {
                                var path = Path.Combine(Server.MapPath("~/Documents/"), myfile);
                                file.SaveAs(path);
                                var fundoc = new FundDocument { Fundid = model.Id, Path = myfile, UserId = getfund.Userid, Created_at = DateTime.Now, Filetype = "Doc" };
                                db.Documents.Add(fundoc);
                                db.SaveChanges();
                            }
                            else if(videofiles.Contains(extension))
                            {
                                var path = Path.Combine(Server.MapPath("~/Videos/"), myfile);
                                file.SaveAs(path);
                                var fundoc = new FundDocument { Fundid = model.Id, Path = myfile, UserId = getfund.Userid, Created_at = DateTime.Now, Filetype = "Video" };
                                db.Documents.Add(fundoc);
                                db.SaveChanges();
                            }

                            
                        }
                    }
                }
            }
            catch(Exception ex)
            { }
            return RedirectToAction("ListOfFunds");
        }

        //Delete a document or image file from the Fund detail
        public ActionResult DeleteDoc(int? docid, int? id)
        {
            var db = new ApplicationDbContext();
            if(docid > 0)
            {
                var getdoc = db.Documents.Where(x => x.Id == docid).FirstOrDefault();
                db.Entry(getdoc).State = EntityState.Deleted;
                db.SaveChanges();
            }

            return RedirectToAction("EditFundDetail", new { id = id });
        }

        //Delete a data from Fund Table
        public ActionResult DeleteFundItem(int? id)
        {
            var db = new ApplicationDbContext();
            if(id > 0)
            {
                var getfunditem = db.Funds.Where(x => x.Id == id).FirstOrDefault();
                db.Entry(getfunditem).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return View();
        }


        // Adding money from the funders
        [HttpPost]
        public ActionResult AddFund(AddFundViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            if(ModelState.IsValid)
            {
                return RedirectToAction("PaymentWithPaypal", "Paypal", new { AmountGiven = model.AmountGiven, Fundid = model.Fundid, Message = model.Message });
                
            }
            return RedirectToAction("Index", "Home");
        }


    }
}