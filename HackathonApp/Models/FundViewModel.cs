using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class FundViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Story")]
        public string Story { get; set; }
        public string FileType { get; set; }
        
        [Required]
        [Display(Name = "Amount Needed")]
        [Range(1, float.MaxValue, ErrorMessage = "Enter numbers starting from 1 and above")]
        public decimal AmountNeeded { get; set; }
        [Display(Name = "Amount Aquired")]
        public decimal? AmountAcquired { get; set; }
        
        [Required]
        [Display(Name = "Target Date")]
        public DateTime DateEnd { get; set; }
        [Display(Name = "Date Posted")]
        public DateTime? DateCreated { get; set; }
        [Display(Name = "Date Edited")]
        public DateTime? DateUpdated { get; set; }
        public string Path { get; set; }
        public bool IsLiked { get; set; }
        public string Message { get; set; }
        public decimal? AmountGiven { get; set; }
        public HttpPostedFileBase[] ImageFile { get; set; }
        public string CommentMessage { get; set; }
        public IList<SupportingDocument> Documents { get; set; }
        public IList<LikeReact> LikeReacts { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
    }

    public class SupportingDocument
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
        public int Fundid { get; set; }
        public string Userid { get; set; }
        
    }

    public class HomeViewModel
    {
        public int Fundid { get; set; }
        public int Id { get; set; }
        public string CommentMessage { get; set; }
        public IList<FundViewModel> FundViews { get; set; }
        public IList<SupportingDocument> Documents { get; set; }
        public IList<LikeReact> LikeReacts { get; set; }
        public IList<CommentViewModel> Comments { get; set; }
        
        public string Message { get; set; }
        
        public decimal AmountGiven { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string FunderName { get; set; }
        public string BenefName { get; set; }
    }

}