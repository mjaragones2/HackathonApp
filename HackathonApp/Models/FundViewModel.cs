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
        [Display(Name = "Amount Needed")]
        [Required]
        [Range(1, float.MaxValue, ErrorMessage = "Enter numbers starting from 1 and above")]
        public decimal? AmountNeeded { get; set; }
        public decimal? AmountAcquired { get; set; }
        [Display(Name = "Target Date")]
        [Required]
        public DateTime? DateEnd { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public HttpPostedFileBase[] File { get; set; }
        public IList<SupportingDocument> Documents { get; set; }
    }

    public class SupportingDocument
    {
        public int Id { get; set; }
        public string Path { get; set; }
    }
}