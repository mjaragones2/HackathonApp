using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class WithdrawPaypalView
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Amount { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public string Status { get; set; }
        public string Fullname { get; set; }
    }

    public class ShowAdminViewModel
    {
        public IList<WithdrawPaypalView> PaypalViews { get; set; }
    }
}