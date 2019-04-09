using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string FullnameReverse { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsDeceased { get; set; }
        public byte Gender { get; set; }
        public bool HasAlerts { get; set; }

        public int Status { get; set; }
        public string StatusColour { get; set; }
    }
}