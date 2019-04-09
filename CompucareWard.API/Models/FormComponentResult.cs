using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompucareWard.API.Models
{
    public class FormComponentResult
    {
        public int FormComponentResultId { get; set; }
      
        public string Result { get; set; }
        public bool IsLocked { get; set; }
        public string Annotation { get; set; }
        public string Caption { get; set; }
        public bool IsVisible { get; set; }
        public string Header { get; set; }
        public string Notes { get; set; }
        public string WarningText { get; set; }        
        public string ExternalIdentifier { get; set; }
        public string ResultUnits { get; set; }
        public string WarningBands { get; set; }
        public string ResultOptions { get; set; }
        public string ResultFormula { get; set; }
        public string VisibilityCondition { get; set; }
        public bool AllowBlank { get; set; }
        public int? Precision { get; set; }
        public decimal? DefaultValue { get; set; }
        public int? LowLimit { get; set; }
        public int? HighLimit { get; set; }
        public int? DefaultRelativeDateValue { get; set; }
        public string WarningColour { get; set; }
        public decimal? WarningResult { get; set; }

        //public byte[] CustomImage { get; set; }
        public byte[] RowVersion { get; set; }

        public int FormResultId { get; set; }
        public FormResult FormResult { get; set; }

        public int? ParentFormComponentResultId { get; set; }
        public virtual FormComponentResult ParentFormComponentResult { get; set; }

        public int FormComponentId { get; set; }
        public FormComponent FormComponent { get; set; }

        public virtual ICollection<FormComponentResult> ChildFormComponentResults { get; set; }
    }
}