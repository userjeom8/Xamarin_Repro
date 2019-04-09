using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompucareWard.API.Models
{
    public class FormResult
    {
        public int FormResultId { get; set; }

        public DateTime? DateTaken { get; set; }
        public int Status { get; set; }
        public string Result { get; set; }
        public string WarningText { get; set; }
        public DateTime CreateDate { get; set; }

        public byte[] RowVersion { get; set; }

        public int PatientId { get; set; }
        public int? FormId { get; set; }
        public int CreateLocationId { get; set; }
        public int CreateUserId { get; set; }
        public int? SignOffUserId { get; set; }
        public int? EpisodeOfCareId { get; set; }
        public int? FormSystemTypeId { get; set; }
        public int? SurgicalBookingId { get; set; }
        public int? CommonBookingId { get; set; }
        public virtual CommonBooking CommonBooking { get; set; }

        public List<FormComponentResult> FormComponentResults { get; set; }
    }
}