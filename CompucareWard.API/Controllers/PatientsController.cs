using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompucareWard.API.Infrastructure;
using CompucareWard.API.Models;
using CompucareWard.API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CompucareWard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly CompucareWardContext _context;

        public PatientsController(CompucareWardContext context)
        {
            _context = context;
        }

        // GET: api/Patients/5/NextOfKin
        [HttpGet("{id}")]
        [Route("{id}/nextofkin")]
        public async Task<IActionResult> GetPatientsPrimaryNextOfKin([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var primaryNextOfKin = await _context.NextOfKins.Where(nok => nok.PatientId == id && nok.SequenceId == 0)
                                                            .Select(nok => new NextOfKinDTO
                                                            {
                                                                NextOfKinId = nok.NextOfKinId,
                                                                Forename = nok.Forename,
                                                                Surname = nok.Surname,
                                                                Title = nok.Title,
                                                                Relationship = nok.Relationship.Name
                                                            })
                                                            .FirstOrDefaultAsync();
            return Ok(primaryNextOfKin);
        }

        // GET: api/Patients/5/Alerts
        [HttpGet("{id}")]
        [Route("{id}/alerts")]
        public async Task<IList<AlertDTO>> GetPatientAlerts([FromRoute] int id)
        {
            return await _context.Alerts.Where(a => a.PatientId == id && (a.DateExpired == null || DateTime.Today > a.DateExpired))
                                        .Select(a => new AlertDTO
                                        {
                                            AlertId = a.AlertId,
                                            AlertReason = new AlertReasonDTO
                                            {
                                                AlertReasonId = a.AlertReasonId,
                                                Name = a.AlertReason.Name,
                                                Ingredient = a.AlertReason.IngredientId.HasValue ? new CodeTableDTO { Id = a.AlertReason.IngredientId.Value, Name = a.AlertReason.Ingredient.Name } : null,
                                                Severity = a.AlertReason.Severity,
                                                Type = a.AlertReason.Type
                                            },
                                            Date = a.Date,
                                            DateExpired = a.DateExpired,
                                            Notes = a.Notes,
                                            PatientId = a.PatientId,
                                            Time = a.Time
                                        })
                                        .OrderByDescending(a => a.AlertReason.Severity)
                                        .ToListAsync();
        }
    }
}