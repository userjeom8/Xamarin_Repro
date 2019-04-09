using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class LocationDTO
    {
        public int LocationId { get; set; }
        public int SiteId { get; set; }
        public string Name { get; set; }
        public PersonDTO ResidentMedicalOfficer { get; set; }
    }
}
