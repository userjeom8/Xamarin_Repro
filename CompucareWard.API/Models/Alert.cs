using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class Alert
    {
        public int AlertId { get; set; }

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Notes { get; set; }
        public DateTime? DateExpired { get; set; }

        public int? PatientId { get; set; }
        public Patient Patient { get; set; }

        public int AlertReasonId { get; set; }
        public virtual AlertReason AlertReason { get; set; }
    }
}
