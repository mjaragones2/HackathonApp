using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class FundTransaction
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Userid { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public string ReceiverId { get; set; }
        public virtual Fund Fund { get; set; }
        public int Fundid { get; set; }
        public decimal? AmountGiven { get; set; }
        public string Message { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}