using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class WithdrawRequest
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Userid { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public decimal? Amount { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}