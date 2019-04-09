using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Parameters
{
    public class HandoverParameter
    {
        public int[] InpatientBookingIds { get; set; }
        public int HandoverNurseId { get; set; }
    }
}
