using CompucareWard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class FormResultSimplifiedDTO : FormResultBaseDTO
    {
        public List<FormComponentResultSimplifiedDTO> FormComponentResults { get; set; }
    }
}
