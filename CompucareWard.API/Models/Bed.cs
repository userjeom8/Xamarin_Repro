using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class Bed
    {
        public int BedId { get; set; }
        public string Name { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
