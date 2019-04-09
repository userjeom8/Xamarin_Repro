using System;
using System.Collections.Generic;
using System.Text;

namespace CompucareWard.Models
{
    public class WarningBandDTO
    {
        public string LowValue { get; set; }
        public string Text { get; set; }
        public string HighValue { get; set; }
        public string Name { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? Gender { get; set; }
        public string Colour { get; set; }
        public decimal? Result { get; set; }
        public bool BoolWarnValue { get; set; }
    }
}
