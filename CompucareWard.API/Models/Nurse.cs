using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class Nurse
    {
        public int NurseId { get; set; }
        public bool WardNurse { get; set; }
        public bool ChargeNurse { get; set; }

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
