using CompucareWard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class InpatientBookingDTO
    {
        public int InpatientBookingId { get; set; }
        public PersonDTO ResponsibleNurse { get; set; }
        public PersonDTO AttendingClinician { get; set; }
        public int CommonBookingId { get; set; }
        public int EpisodeOfCareId { get; set; }

        public DateTime Admission { get; set; }
        public DateTime Discharge { get; set; }

        public BedDTO CurrentBed { get; set; }
        public PatientDTO Patient { get; set; }
        public FormResultValue NEWSCurrent { get; set; }
        public FormResultValue NEWSPrevious { get; set; }
        public int? ObservationFrequencyInMinutes { get; set; }
        public DateTime? ObservationDue { get; set; }
    }
}
