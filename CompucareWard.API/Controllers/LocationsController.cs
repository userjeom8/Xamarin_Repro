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
    public class LocationsController : ControllerBase
    {
        private readonly CompucareWardContext _context;

        public LocationsController(CompucareWardContext context) => _context = context;

        // GET: api/Locations
        [HttpGet]
        public IEnumerable<CodeTableDTO> GetLocations() =>_context.Locations.Where(l => l.Beds.Any()).Select(l => new CodeTableDTO { Id = l.LocationId, Name = l.Name });
    }
}