using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class PatientSettingsDTO
    {
        public int AgeAtWhichCeasesToBeChild { get; set; }

        public string StatusUnknownColour { get; set; }
        public string StatusChildFemaleColour { get; set; }
        public string StatusChildMaleColour { get; set; }
        public string StatusChildUnknownColour { get; set; }
        public string StatusFemaleColour { get; set; }
        public string StatusMaleColour { get; set; }
        public string StatusDeceasedColour { get; set; }
        public string StatusNewColour { get; set; }
    }
}