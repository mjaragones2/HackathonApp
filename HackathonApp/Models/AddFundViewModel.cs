using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class AddFundViewModel
    {
        public int Id { get; set; }
        public int Fundid { get; set; }
        public int FundTransId { get; set; }
        public string Userid { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public decimal AmountGiven { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string FunderName { get; set; }
        public string BenefName { get; set; }
    }
}