using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class FundDocument
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public virtual Fund Fund { get; set; }
        public int Fundid { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public DateTime? Created_at { get; set; }
    }
}