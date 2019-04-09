using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompucareWard.API.Models
{
    public class Person
    {
        public int PersonId { get; set; }

        public int? SiteId { get; set; }

        public string FullnameReverse { get; set; }
        public string WorkPhone { get; set; }
        public string MainPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}