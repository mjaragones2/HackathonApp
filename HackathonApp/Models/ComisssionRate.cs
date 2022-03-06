using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class ComisssionRate
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}