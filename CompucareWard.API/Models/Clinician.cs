using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class Clinician
    {
        public int ClinicianId { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
