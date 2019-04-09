using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class NEWSSettingsDTO
    {
        public string GuidanceDocumentURL { get; set; }
        public List<NEWSChartThresholdTrigger> NEWSChartThresholdsTriggers { get; set; } = new List<NEWSChartThresholdTrigger>();
    }
}
