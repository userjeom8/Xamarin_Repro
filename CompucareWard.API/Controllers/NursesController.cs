using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompucareWard.API.Infrastructure;
using CompucareWard.API.Models;
using Microsoft.AspNetCore.Authorization;
using CompucareWard.API.DTOs;

namespace CompucareWard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly CompucareWardContext _context;

        public NursesController(CompucareWardContext context)
        {
            _context = context;
        }

        // GET: api/nurses/2
        [HttpGet("excludeNurseId")]
        [Route("excludenurse/{excludeNurseId}/site/{siteId}")]
        public async Task<IList<CodeTableDTO>> GetNursesExcluding(int excludeNurseId, int siteId)
        {
            return await _context.Nurses.Where(n => n.WardNurse && n.NurseId != excludeNurseId && (n.Person.SiteId == null || n.Person.SiteId == siteId))
                                        .Select(l => new CodeTableDTO { Id = l.NurseId, Name = l.Person.FullnameReverse }).ToListAsync();
        }
    }
}