using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompucareWard.API.Models
{
    public class FormSubcomponent
    {
        public int FormSubcomponentId { get; set; }

        public int Sequence { get; set; }
        public string Caption { get; set; }

        public int ParentFormComponentId { get; set; }
        public virtual FormComponent ParentFormComponent { get; set; }

        public int FormComponentId { get; set; }
        public FormComponent FormComponent { get; set; }
    }
}