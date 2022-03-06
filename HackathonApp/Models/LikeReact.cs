using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class LikeReact
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Userid { get; set; }
        public bool IsLiked { get; set; }
        public virtual Fund Fund { get; set; }
        public int Fundid { get; set; }
    }
}