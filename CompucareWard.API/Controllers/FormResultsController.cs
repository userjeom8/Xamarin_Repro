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
using System.Xml.Linq;
using CompucareWard.API.Services;

namespace CompucareWard.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FormResultsController : ControllerBase
    {
        private readonly CompucareWardContext _context;
        private readonly IInpatientBookingServiceInternal _inpatientBookingService;
        public static int NEWSSystemTypeId = 59;

        public FormResultsController(CompucareWardContext context, IInpatientBookingServiceInternal inpatientBookingService)
        {
            _context = context;
            _inpatientBookingService = inpatientBookingService;
        }

        [HttpGet]
        [Route("commonbooking/{commonbookingid}")]
        public async Task<IList<FormResultSimplifiedDTO>> GetNEWSResultsForBooking(int commonBookingId)
        {
            return await _context.FormResults
                                 .Where(p => p.CommonBookingId == commonBookingId && p.FormComponentResults.Any(frc => frc.FormComponent.SystemTypeId == NEWSSystemTypeId))
                                 .Select(fr => new FormResultSimplifiedDTO
                                 {
                                     FormResultId = fr.FormResultId,
                                     CommonBookingId = fr.CommonBookingId,
                                     CreateDate = fr.CreateDate,
                                     DateTaken = fr.DateTaken,
                                     EpisodeOfCareId = fr.EpisodeOfCareId,
                                     FormComponentResults = fr.FormComponentResults.OrderBy(fcr => fcr.ParentFormComponentResultId.HasValue ? fcr.FormComponentResultId : 0)
                                     .Select(fcr => new FormComponentResultSimplifiedDTO
                                     {
                                         Caption = fcr.Caption,
                                         FormComponentId = fcr.FormComponentId,
                                         FormComponentResultId = fcr.FormComponentResultId,
                                         FormResultId = fcr.FormResultId,
                                         FormComponentSystemTypeId = fcr.FormComponent.SystemTypeId,
                                         Header = fcr.Header,
                                         IsVisible = fcr.IsVisible,
                                         ParentFormComponentResultId = fcr.ParentFormComponentResultId,
                                         Result = fcr.Result,
                                         ResultUnits = fcr.ResultUnits,
                                         ResultOptions = fcr.ResultOptions,
                                         WarningColour = fcr.WarningColour,
                                         WarningText = fcr.WarningText
                                     }).ToList(),
                                     PatientId = fr.PatientId,
                                     Result = fr.Result,
                                     Status = fr.Status,
                                     SurgicalBookingId = fr.SurgicalBookingId,
                                     WarningText = fr.WarningText
                                 })
                                 .OrderByDescending(fr => fr.CreateDate)
                                 .ToListAsync();
        }

        [HttpGet]
        [Route("NEW/Patient/{patientId}/Booking/{bookingId}/EpisodeOfCare/{episodeOfCareId}")]
        public async Task<IActionResult> GetNewFormResult(int patientId, int bookingId, int episodeOfCareId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newsComponent = await _context.FormComponents
                                              .Include(fc => fc.FormSubcomponents).ThenInclude(fsc => fsc.FormComponent)
                                              .FirstOrDefaultAsync(fc => fc.SystemTypeId == NEWSSystemTypeId);

            var copyFromPreviousComponents = newsComponent.FormSubcomponents.Where(fsc => fsc.FormComponent.CopyResultFromPrevious).Select(fsc => fsc.FormComponentId).ToArray();

            var previousValues = await _context.FormResults.Where(fr => fr.CommonBookingId == bookingId && fr.PatientId == patientId && fr.EpisodeOfCareId == episodeOfCareId)
                                                           .OrderByDescending(xx => xx.CreateDate)
                                                           .Select(xx => new
                                                           {
                                                               xx.FormResultId,
                                                               Values = xx.FormComponentResults.Where(fcr => copyFromPreviousComponents.Contains(fcr.FormComponentId))
                                                                                               .Select(fcr => new { fcr.FormComponentId, fcr.Result }).ToList()
                                                           })
                                                           .FirstOrDefaultAsync();

            if (newsComponent == null)
                return NotFound();

            var formResult = new FormResultDTO()
            {
                PatientId = patientId,
                EpisodeOfCareId = episodeOfCareId,
                CommonBookingId = bookingId,
                Status = 1,
                FormComponentResults = new List<FormComponentResultDTO> { GetFormResultFromFormComponent(newsComponent, previousValues?.Values.Select(pv => (pv.FormComponentId, pv.Result)).ToList()) }
            };

            return Ok(formResult);
        }

        FormComponentResultDTO GetFormResultFromFormComponent(FormComponent newsComponent, List<(int FormComponentId, string Result)> previousValues)
        {
            return new FormComponentResultDTO()
            {
                IsVisible = true,
                FormComponentSystemTypeId = newsComponent.SystemTypeId,
                Caption = newsComponent.Name,
                FormComponentId = newsComponent.FormComponentId,
                ResultFormula = newsComponent.ResultFormula,
                ResultOptions = newsComponent.ResultOptions,
                ResultUnits = newsComponent.ResultUnits,
                WarningBands = newsComponent.WarningBands,
                Precision = newsComponent.Precision,
                DefaultValue = newsComponent.DefaultValue,
                LowLimit = newsComponent.LowLimit,
                HighLimit = newsComponent.HighLimit,
                DefaultRelativeDateValue = newsComponent.DefaultRelativeDateValue,
                AllowBlank = newsComponent.AllowBlank,
                ChildFormComponentResults = newsComponent.FormSubcomponents.OrderBy(fsc => fsc.Sequence)
                                                                           .Select(fsc => GetComponentResultFromSubcomponent(fsc, previousValues?.Where(pv => fsc.FormComponentId == pv.FormComponentId)
                                                                                                                                                 .Select(pv => pv.Result)
                                                                                                                                                 .SingleOrDefault())).ToList()
            };
        }

        FormComponentResultDTO GetComponentResultFromSubcomponent(FormSubcomponent subComponent, string result)
        {
            return new FormComponentResultDTO()
            {
                FormComponentId = subComponent.FormComponentId,
                IsVisible = true,
                Caption = subComponent.Caption,
                FormComponentSystemTypeId = subComponent.FormComponent.SystemTypeId,
                Result = result,
                ResultFormula = subComponent.FormComponent.ResultFormula,
                ResultOptions = subComponent.FormComponent.ResultOptions,
                ResultUnits = subComponent.FormComponent.ResultUnits,
                WarningBands = subComponent.FormComponent.WarningBands,
                Precision = subComponent.FormComponent.Precision,
                DefaultValue = subComponent.FormComponent.DefaultValue,
                LowLimit = subComponent.FormComponent.LowLimit,
                HighLimit = subComponent.FormComponent.HighLimit,
                DefaultRelativeDateValue = subComponent.FormComponent.DefaultRelativeDateValue,
                AllowBlank = subComponent.FormComponent.AllowBlank
            };
        }

        // GET: api/FormResults/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFormResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formResult = await _context.FormResults.Select(fr => new FormResultDTO() { FormResultId = fr.FormResultId }).FirstOrDefaultAsync(fr => fr.FormResultId == id);

            if (formResult == null)
                return NotFound();

            return Ok(formResult);
        }

        // PUT: api/FormResults/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormResult([FromRoute] int id, [FromBody] FormResult formResult)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != formResult.FormResultId)
                return BadRequest();

            _context.Entry(formResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FormResultExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/FormResults
        [HttpPost]
        public async Task<IActionResult> PostFormResult([FromBody] FormResultDTO formResult)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = MapFormResult(formResult);
            _context.FormResults.Add(entity);

            var inpatient = await _context.InpatientBookings.Select(ip => new InpatientBooking()
            {
                InpatientBookingId = ip.InpatientBookingId,
                CommonBookingId = ip.CommonBookingId,
                ObservationFrequencyInMinutes = ip.ObservationFrequencyInMinutes,
                ObservationDue = ip.ObservationDue
            }).FirstAsync(ip => ip.CommonBookingId == formResult.CommonBookingId);

            if (inpatient.ObservationFrequencyInMinutes.HasValue)
                inpatient.ObservationDue = DateTime.Now.AddMinutes(inpatient.ObservationFrequencyInMinutes.Value);
            else
                inpatient.ObservationDue = null;

            _context.Entry(inpatient).Property(nameof(InpatientBooking.ObservationDue)).IsModified = true;
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFormResult", new { id = entity.FormResultId }, formResult);
        }

        FormResult MapFormResult(FormResultDTO dto)
        {
            var entity = new FormResult
            {
                CommonBookingId = dto.CommonBookingId,
                CreateDate = DateTime.Now,
                CreateLocationId = 1,
                CreateUserId = 1,
                DateTaken = DateTime.Today,
                EpisodeOfCareId = dto.EpisodeOfCareId,
                FormComponentResults = new List<FormComponentResult>(),
                FormId = dto.FormId,
                FormResultId = dto.FormResultId,
                FormSystemTypeId = dto.FormSystemTypeId,
                PatientId = dto.PatientId,
                Result = dto.Result,
                RowVersion = dto.RowVersion,
                SignOffUserId = dto.SignOffUserId,
                Status = dto.Status,
                SurgicalBookingId = dto.SurgicalBookingId,
                WarningText = dto.WarningText
            };

            foreach (var formComponentResult in dto.FormComponentResults)
            {
                var componentResultEntity = MapFormComponentResult(formComponentResult, null);
                entity.FormComponentResults.Add(componentResultEntity);

                foreach (var childFormComponentResult in formComponentResult.ChildFormComponentResults)
                    entity.FormComponentResults.Add(MapFormComponentResult(childFormComponentResult, componentResultEntity));
            }

            return entity;
        }

        FormComponentResult MapFormComponentResult(FormComponentResultDTO dto, FormComponentResult parent)
        {
            var entity = new FormComponentResult()
            {
                AllowBlank = dto.AllowBlank,
                Annotation = dto.Annotation,
                Caption = dto.Caption,
                DefaultRelativeDateValue = dto.DefaultRelativeDateValue,
                DefaultValue = dto.DefaultValue,
                ExternalIdentifier = dto.ExternalIdentifier,
                FormComponentId = dto.FormComponentId,
                FormComponentResultId = dto.FormComponentResultId,
                Header = dto.Header,
                HighLimit = dto.HighLimit,
                IsLocked = dto.IsLocked,
                IsVisible = dto.IsVisible,
                LowLimit = dto.LowLimit,
                Notes = dto.Notes,
                ParentFormComponentResult = parent,
                Precision = dto.Precision,
                Result = dto.Result,
                ResultFormula = dto.ResultFormula,
                ResultOptions = dto.ResultOptions,
                ResultUnits = dto.ResultUnits,
                RowVersion = dto.RowVersion,
                VisibilityCondition = dto.VisibilityCondition,
                WarningBands = dto.WarningBands,
                WarningColour = dto.WarningColour,
                WarningResult = dto.WarningResult,
                WarningText = dto.WarningText,
            };

            return entity;
        }

        // DELETE: api/FormResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormResult([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var formResult = await _context.FormResults.FindAsync(id);

            if (formResult == null)
                return NotFound();

            _context.FormResults.Remove(formResult);
            await _context.SaveChangesAsync();
            return Ok(formResult);
        }

        private bool FormResultExists(int id) => _context.FormResults.Any(e => e.FormResultId == id);
    }
}