using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class FundComments
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Userid { get; set; }
        public string Comment { get; set; }
        public virtual Fund Fund { get; set; }
        public int Fundid { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}