using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class AlertReason
    {
        public int AlertReasonId { get; set; }

        public int Type { get; set; }
        public string Name { get; set; }
        public int Severity { get; set; }

        public int? IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
