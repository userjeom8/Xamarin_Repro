using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompucareWard.API.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public int? ResidentMedicalOfficerId { get; set; }

        public ICollection<Bed> Beds { get; set; }
        public Clinician ResidentMedicalOfficer { get; set; }
    }
}