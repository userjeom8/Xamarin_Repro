using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class CommonBooking
    {
        public int CommonBookingId { get; set; }
        public int EpisodeOfCareId { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public virtual ICollection<FormResult> FormResults { get; set; }
    }
}
