using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class Fund
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Story { get; set; }
        public decimal? AmountNeeded { get; set; }
        public decimal? AmountAcquired { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Userid { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}