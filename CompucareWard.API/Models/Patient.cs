using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FullnameReverse { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsDeceased { get; set; }
        public byte Gender { get; set; }

        public virtual ICollection<Alert> Alerts { get; set; }
        public virtual ICollection<NextOfKin> NextOfKins { get; set; }
    }
}
