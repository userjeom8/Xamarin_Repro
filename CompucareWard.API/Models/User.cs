using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompucareWard.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}