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
    [Authorize]
    public class FundController : Controller
    {
        // GET: Fund

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyProject()
        {
            var userid = User.Identity.GetUserId();
            var db = new ApplicationDbContext();

            HomeViewModel model = new HomeViewModel();
            List<FundViewModel> viewModels = new List<FundViewModel>();
            List<SupportingDocument> supportingDocuments = new List<SupportingDocument>();
            var listoffunds = db.Funds.Where(x => x.Userid == userid && x.Status != "Closed").ToList();
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
                    var countreact = db.Like.Where(x => x.Fundid == fund.Id && x.IsLiked == true).ToList();
                    var getreact = db.Like.Where(x => x.Userid == userid && x.Fundid == fund.Id).FirstOrDefault();
                    var getonepic = db.Documents.Where(x => x.Fundid == fund.Id && x.Filetype == "Image").FirstOrDefault();
                    if (getonepic != null)
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype, CountLike = countreact.Count, Userid = fund.Userid });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false, CountLike = countreact.Count, Userid = fund.Userid });
                        }

                    }
                    else
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype, CountLike = countreact.Count, Userid = fund.Userid });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, IsLiked = false, Path = getonepic.Path, CountLike = countreact.Count, Userid = fund.Userid });
                        }
                    }

                }
            }
            model.Documents = supportingDocuments;
            model.FundViews = viewModels;
            return View(model);
        }

        [HttpPost]
        public ActionResult MyProject(HomeViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            var request = new WithdrawRequest
            {
                Amount = model.Amount,
                Contact = model.ContactInfo,
                DateCreated = DateTime.Now,
                Email = model.Email,
                Status = "Pending",
                Userid = userid
            };
            db.WithdrawReq.Add(request);
            db.SaveChanges();

            List<FundViewModel> viewModels = new List<FundViewModel>();
            List<SupportingDocument> supportingDocuments = new List<SupportingDocument>();
            var listoffunds = db.Funds.Where(x => x.Userid == userid && x.Status != "Closed").ToList();
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
                    var countreact = db.Like.Where(x => x.Fundid == fund.Id && x.IsLiked == true).ToList();
                    var getreact = db.Like.Where(x => x.Userid == userid && x.Fundid == fund.Id).FirstOrDefault();
                    var getonepic = db.Documents.Where(x => x.Fundid == fund.Id && x.Filetype == "Image").FirstOrDefault();
                    if (getonepic != null)
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype, CountLike = countreact.Count });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = false, CountLike = countreact.Count });
                        }

                    }
                    else
                    {
                        if (getreact != null)
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, Path = getonepic.Path, IsLiked = getreact.IsLiked, FileType = getonepic.Filetype, CountLike = countreact.Count });
                        }
                        else
                        {
                            viewModels.Add(new FundViewModel { Id = fund.Id, AmountNeeded = fund.AmountNeeded.Value, AmountAcquired = fund.AmountAcquired, DateCreated = fund.DateCreated, Title = fund.Title, Story = fund.Story, DateUpdated = fund.DateUpdated, DateEnd = fund.DateEnd.Value, IsLiked = false, Path = getonepic.Path, CountLike = countreact.Count });
                        }
                    }

                }
            }
            model.Documents = supportingDocuments;
            model.FundViews = viewModels;
            return View(model);
        }

        public ActionResult MyProjectDetail(int? id)
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
                            viewModel.Documents.Add(new SupportingDocument { Id = doc.Id, Path = doc.Path, FileType = doc.Filetype, Fundid = doc.Fundid });
                        }

                    }
                }
            }
            return View(viewModel);
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
                        var videofile = new[] { ".mp4", ".mkv", ".ogg" };
                        var imagefiles = new[] { ".jpg", ".jpeg", ".png", "jiff", ".gif" };
                        var docsfile = new[] { ".docx", ".pdf", ".ppt", ".pptx" };
                        if(videofile.Contains(extension))
                        {
                            var path = Path.Combine(Server.MapPath("~/Videos/"), name);
                            file.SaveAs(path);

                            var docs = new FundDocument { Created_at = DateTime.Now, Fundid = fund.Id, Path = name, UserId = getuser, Filetype = "Video" };
                            db.Documents.Add(docs);
                            db.SaveChanges();
                        }
                        else if(imagefiles.Contains(extension))
                        {
                            var path = Path.Combine(Server.MapPath("~/images/"), name);
                            file.SaveAs(path);

                            var docs = new FundDocument { Created_at = DateTime.Now, Fundid = fund.Id, Path = name, UserId = getuser, Filetype = "Image" };
                            db.Documents.Add(docs);
                            db.SaveChanges();
                        }
                        else if(docsfile.Contains(extension))
                        {
                            var path = Path.Combine(Server.MapPath("~/Documents/"), name);
                            file.SaveAs(path);

                            var docs = new FundDocument { Created_at = DateTime.Now, Fundid = fund.Id, Path = name, UserId = getuser, Filetype = "Doc" };
                            db.Documents.Add(docs);
                            db.SaveChanges();
                        }
                        
                    }
                }
                
            }
            return RedirectToAction("MyProject");
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
            viewModel.Comments = new List<CommentViewModel>();
            if(id > 0)
            {
                var getreact = db.Like.Where(x => x.Fundid == id).FirstOrDefault();
                var getfund = db.Funds.Where(x => x.Id == id).FirstOrDefault();
                if(getfund != null)
                {
                    var getusername = db.Users.Where(x => x.Id == getfund.Userid).FirstOrDefault();
                    viewModel.Id = getfund.Id;
                    viewModel.Title = getfund.Title;
                    viewModel.Story = getfund.Story;
                    viewModel.AmountNeeded = getfund.AmountNeeded.Value;
                    viewModel.AmountAcquired = getfund.AmountAcquired;
                    viewModel.DateCreated = getfund.DateCreated;
                    viewModel.DateEnd = getfund.DateEnd.Value;
                    viewModel.DateUpdated = getfund.DateUpdated;
                    viewModel.CountLike = getfund.Counter;
                    viewModel.Userid = getusername.UserName;
                    if(getreact != null)
                    {
                        viewModel.IsLiked = true;
                    }
                    var getcomments = db.Comments.Where(x => x.Fundid == getfund.Id).ToList();
                    if(getcomments.Count > 0)
                    {
                        foreach(var comm in getcomments)
                        {
                            var getuser = db.Users.Where(x => x.Id == getfund.Userid).FirstOrDefault();
                            if (getuser.Image != null)
                            {
                                viewModel.Comments.Add(new CommentViewModel
                                {
                                    Comment = comm.Comment,
                                    DateCreated = comm.DateCreated,
                                    Fullname = getuser.FirstName + " " + getuser.LastName,
                                    Id = comm.Id,
                                    Path = getuser.Image
                                });
                            }
                            else
                            {
                                viewModel.Comments.Add(new CommentViewModel
                                {
                                    Comment = comm.Comment,
                                    DateCreated = comm.DateCreated,
                                    Fullname = getuser.FirstName + " " + getuser.LastName,
                                    Id = comm.Id,
                                    Path = "user.png"
                                });
                            }
                            
                        }
                    }

                    var getdocs = db.Documents.Where(x => x.Fundid == getfund.Id).ToList();
                    if(getdocs.Count > 0)
                    {
                        foreach(var doc in getdocs)
                        {
                            viewModel.Documents.Add(new SupportingDocument { Id = doc.Id, Path = doc.Path, FileType = doc.Filetype, Fundid = doc.Fundid });
                        }
                        
                    }
                }
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(FundViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();

            if (model.CommentMessage != null)
            {
                var comm = new FundComments
                {
                    Comment = model.CommentMessage,
                    DateCreated = DateTime.Now,
                    Fundid = model.Id,
                    Userid = userid, 
                    DateUpdated = DateTime.Now
                };
                db.Comments.Add(comm);
                db.SaveChanges();
            }

            return RedirectToAction("FundDetail", new { id = model.Id });
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
        public ActionResult AddFund(HomeViewModel model)
        {
            var db = new ApplicationDbContext();
            var userid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                Session["fundid"] = model.Id;
                Session["messa"] = model.Message;
                Session["amount"] = model.AmountGiven;
                return RedirectToAction("PaymentWithPaypal", "Paypal", new { AmountGiven = model.AmountGiven, Fundid = model.Id, Message = model.Message });
            }
            return RedirectToAction("Index", "Home");
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
                            var videofiles = new[] { ".WebM", ".mp4", ".ogg" };
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


        public ActionResult RequestToWithdraw()
        {
            var db = new ApplicationDbContext();
            return View();
        }

    }
}