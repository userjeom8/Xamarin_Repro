using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class BedBooking
    {
        public int ScheduleId { get; set; }
        public int InaptientBookingId { get; set; }

        public int Status { get; set; }

        public int BedId { get; set; }
        public Bed Bed { get; set; }
    }
}
