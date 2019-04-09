using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class NextOfKinDTO
    {
        public int NextOfKinId { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public string Title { get; set; }

        public string FullnameReverse
        {
            get => $"{Surname?.ToUpper()} {Forename} ({Title})";
        }

        public string Relationship { get; set; }
    }
}