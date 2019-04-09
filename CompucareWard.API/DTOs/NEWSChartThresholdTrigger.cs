using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class NEWSChartThresholdTrigger
    {
        public int LowLimit { get; set; }
        public int HighLimit { get; set; }
        public int ObservationFrequencyInMinutes { get; set; }
        public bool Has3InSingleResult { get; set; }
        public string ResponseText { get; set; }
    }
}
