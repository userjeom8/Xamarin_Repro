using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class ContactDTO
    {
        public int PersonId { get; set; }
        public string FullnameReverse { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsEmail { get; set; }
    }
}