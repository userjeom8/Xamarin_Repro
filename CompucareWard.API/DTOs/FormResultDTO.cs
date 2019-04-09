using CompucareWard.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.DTOs
{
    public class FormResultDTO : FormResultBaseDTO
    {
        public byte[] RowVersion { get; set; }

        public int? FormId { get; set; }
        public int CreateLocationId { get; set; }
        public int CreateUserId { get; set; }
        public int? SignOffUserId { get; set; }
        public int? FormSystemTypeId { get; set; }

        public List<FormComponentResultDTO> FormComponentResults { get; set; }
    }
}
