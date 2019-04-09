using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class FormComponentDTO
    {
        public int FormComponentId { get; set; }

        public int? FormCategoryId { get; set; }

        public string Name { get; set; }
        public string Notes { get; set; }
        public int ResultType { get; set; }
        public string ResultUnits { get; set; }
        public string ResultOptions { get; set; }
        public bool AllowBlank { get; set; }
        public string WarningBands { get; set; }
        public int? SystemTypeId { get; set; }
        public int? DefaultRelativeDateValue { get; set; }
        public string ResultFormula { get; set; }
        public DateTime? StopDate { get; set; }
        public int? Precision { get; set; }
        public decimal? DefaultValue { get; set; }
        public int? LowLimit { get; set; }
        public int? HighLimit { get; set; }
        public bool CopyResultFromPrevious { get; set; }
        public string Abbreviation { get; set; }
    }
}
