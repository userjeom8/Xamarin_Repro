using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class FormComponentResultBaseDTO
    {
        public int FormComponentResultId { get; set; }
        public int FormComponentId { get; set; }
        public string Result { get; set; }
        public string Caption { get; set; }
        public bool IsVisible { get; set; }
        public string Header { get; set; }
        public string WarningText { get; set; }
        public string ResultUnits { get; set; }
        public string WarningColour { get; set; }
        public int FormResultId { get; set; }
        public int? ParentFormComponentResultId { get; set; }
        public int? FormComponentSystemTypeId { get; set; }
        public string ResultOptions { get; set; }
    }
}
