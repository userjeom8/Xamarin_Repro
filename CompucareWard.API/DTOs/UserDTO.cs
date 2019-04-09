using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FullnameReverse { get; set; }

        public LocationDTO Location { get; set; }
        public int PersonId { get; set; }
        public string LicenceNumber { get; set; }
        public string SystemType { get; set; }

        public int? NurseId { get; set; }
        public int? ClinicianId { get; set; }
        public byte[] Thumbnail { get; set; }
    }
}
