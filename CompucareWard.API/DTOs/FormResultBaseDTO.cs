using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class FormResultBaseDTO
    {
        public int FormResultId { get; set; }

        public DateTime? DateTaken { get; set; }
        public int Status { get; set; }
        public string Result { get; set; }
        public string WarningText { get; set; }
        public DateTime CreateDate { get; set; }

        public int PatientId { get; set; }
        public int? EpisodeOfCareId { get; set; }
        public int? SurgicalBookingId { get; set; }
        public int? CommonBookingId { get; set; }
    }
}
