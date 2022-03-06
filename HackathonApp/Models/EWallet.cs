﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class EWallet
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Userid { get; set; }
        public decimal Balance { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}