
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Models
{
    public class NextOfKin
    {
        public int NextOfKinId { get; set; }

        public int SequenceId { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string MainPhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? Dob { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public int? RelationshipId { get; set; }
        public virtual Relationship Relationship { get; set; }
    }
}
