using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class InpatientBooking
    {
        public int InpatientBookingId { get; set; }

        public DateTime? Admitted { get; set; }
        public DateTime? Discharged { get; set; }

        public DateTime Admission { get; set; }
        public DateTime Discharge { get; set; }

        public int? ResponsibleNurseId { get; set; }
        public virtual Nurse ResponsibleNurse { get; set; }

        public int? AttendingClinicianId { get; set; }
        public virtual Clinician AttendingClinician { get; set; }

        public int CommonBookingId { get; set; }
        public virtual CommonBooking CommonBooking { get; set; }

        public virtual ICollection<BedBooking> BedBookings { get; set; }

        public int? ObservationFrequencyInMinutes { get; set; }
        public DateTime? ObservationDue { get; set; }
    }
}
