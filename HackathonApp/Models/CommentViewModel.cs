using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonApp.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Fullname { get; set; }
        public string Userid { get; set; }
        public DateTime? DateCreated { get; set; }

    }
}