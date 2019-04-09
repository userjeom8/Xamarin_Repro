using CompucareWard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class FormComponentResultDTO : FormComponentResultBaseDTO
    {
        public bool IsLocked { get; set; }
        public string Annotation { get; set; }
        public string Notes { get; set; }
        public string ExternalIdentifier { get; set; }
        public string WarningBands { get; set; }
        public string ResultFormula { get; set; }
        public string VisibilityCondition { get; set; }
        public bool AllowBlank { get; set; }
        public int? Precision { get; set; }
        public decimal? DefaultValue { get; set; }
        public int? LowLimit { get; set; }
        public int? HighLimit { get; set; }
        public int? DefaultRelativeDateValue { get; set; }
        public decimal? WarningResult { get; set; }

        //public byte[] CustomImage { get; set; }
        public byte[] RowVersion { get; set; }

        //public int FormComponentId { get; set; }
        //public FormComponentDTO FormComponent { get; set; }

        public IList<FormComponentResultDTO> ChildFormComponentResults { get; set; }
    }
}
