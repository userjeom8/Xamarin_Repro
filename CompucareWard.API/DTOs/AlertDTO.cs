using CompucareWard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class AlertDTO
    {
        public int AlertId { get; set; }

        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Notes { get; set; }
        public DateTime? DateExpired { get; set; }

        public int? PatientId { get; set; }

        public AlertReasonDTO AlertReason { get; set; }
    }
}
