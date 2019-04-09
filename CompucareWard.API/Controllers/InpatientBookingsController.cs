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
using System.Xml.Linq;
using System.Linq.Expressions;
using StreetsHeaver.Common.Linq;
using CompucareWard.API.Services;
using CompucareWard.API.Parameters;

namespace CompucareWard.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class InpatientBookingsController : ControllerBase
    {
        private readonly CompucareWardContext _context;
        private readonly IInpatientBookingServiceInternal _inpatientBookingService;
        private readonly IGlobalSettingsService _globalSettingsService;

        public InpatientBookingsController(CompucareWardContext context, IInpatientBookingServiceInternal inpatientBookingService, IGlobalSettingsService globalSettingsService)
        {
            _context = context;
            _inpatientBookingService = inpatientBookingService;
            _globalSettingsService = globalSettingsService;
        }

        // GET: api/InpatientBookings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInpatientBooking([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _context.InpatientBookings.Select(_inpatientBookingProjection).FirstOrDefaultAsync(ip => ip.InpatientBookingId == id);

            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        // GET: api/InpatientBookings
        [HttpGet()]
        [Route("responsiblenurse/{responsibleNurseId}/clinician/{clinicianid}")]
        public async Task<IEnumerable<InpatientBookingDTO>> GetInpatientBookingsByUser(int responsibleNurseId, int clinicianId)
        {
            var patientSettings = await _globalSettingsService.GetPatientSettings();

            return (await _context.InpatientBookings.Where(ip => ((ip.ResponsibleNurseId == responsibleNurseId) || (ip.AttendingClinicianId == clinicianId))
                                                                  && ip.Admitted.HasValue && !ip.Discharged.HasValue)
                                                    .Select(_inpatientBookingProjection)
                                                    .ToListAsync()).Select(ib => _globalSettingsService.ApplyStatus(patientSettings, ib)).ToList();
        }

        // GET: api/InpatientBookings
        [HttpGet("{locationId}")]
        [Route("location/{locationId}")]
        public async Task<IEnumerable<InpatientBookingDTO>> GetInpatientBookings(int locationId)
        {
            var patientSettings = await _globalSettingsService.GetPatientSettings();

            return (await _context.InpatientBookings.Where(ip => ip.Admitted.HasValue && !ip.Discharged.HasValue && ip.BedBookings.Any(bb => bb.Status == 2 && bb.Bed.LocationId == locationId))
                                                    .Select(_inpatientBookingProjection)
                                                    .ToListAsync()).Select(ib => _globalSettingsService.ApplyStatus(patientSettings, ib)).ToList();
        }

        // GET: api/InpatientBookings
        [HttpGet("{responsibleNurseId}")]
        [Route("reminders/{responsibleNurseId}")]
        public async Task<IEnumerable<InpatientBookingDTO>> GetInpatientBookingsReminders(int responsibleNurseId)
        {
            var patientSettings = await _globalSettingsService.GetPatientSettings();

            return (await _context.InpatientBookings.Where(ip => ip.ResponsibleNurseId == responsibleNurseId && ip.Admitted.HasValue && !ip.Discharged.HasValue)
                                                    .Select(_inpatientBookingProjection).OrderBy(ip => ip.ObservationDue)
                                                    .ToListAsync()).Select(ib => _globalSettingsService.ApplyStatus(patientSettings, ib)).ToList();
        }

        // PUT: api/InpatientBookings/handover
        [HttpPut]
        [Route("handover")]
        public async Task<IActionResult> PutInpatientBookingNurses([FromBody] HandoverParameter handover)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentBookings = (await _context.InpatientBookings.Where(ip => handover.InpatientBookingIds.Contains(ip.InpatientBookingId))
                                                                   .Select(ip => new InpatientBooking()
                                                                   {
                                                                       InpatientBookingId = ip.InpatientBookingId,
                                                                       ResponsibleNurseId = ip.ResponsibleNurseId,
                                                                   }).ToListAsync());

            foreach (var booking in currentBookings)
            {
                booking.ResponsibleNurseId = handover.HandoverNurseId;
                _context.Entry(booking).Property(b => b.ResponsibleNurseId).IsModified = true;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PUT: api/InpatientBookings/frequency/1/30
        [HttpPut]
        [Route("frequency/{inpatientBookingId}/{frequencyInMinutes}")]
        public async Task<IActionResult> PutInpatientBookingFrequency(int inpatientBookingId, int frequencyInMinutes)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _inpatientBookingService.UpdateObservationFrequency(inpatientBookingId, frequencyInMinutes);
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }

        static Expression<Func<InpatientBooking, InpatientBookingDTO>> _inpatientBookingProjection = ip => new InpatientBookingDTO
        {
            InpatientBookingId = ip.InpatientBookingId,
            CommonBookingId = ip.CommonBookingId,
            EpisodeOfCareId = ip.CommonBooking.EpisodeOfCareId,
            AttendingClinician = ip.AttendingClinicianId.HasValue ? new PersonDTO { Id = ip.AttendingClinicianId.Value, PersonId = ip.AttendingClinician.PersonId, FullnameReverse = ip.AttendingClinician.Person.FullnameReverse } : null,
            ResponsibleNurse = ip.ResponsibleNurseId.HasValue ? new PersonDTO { Id = ip.ResponsibleNurseId.Value, PersonId = ip.ResponsibleNurse.PersonId, FullnameReverse = ip.ResponsibleNurse.Person.FullnameReverse } : null,
            Admission = ip.Admission,
            Discharge = ip.Discharge,
            CurrentBed = ip.BedBookings.Where(bb => bb.Status == 2)
                                       .Select(bb => new BedDTO
                                       {
                                           BedId = bb.BedId,
                                           Name = bb.Bed.Name,
                                           Location = new LocationDTO
                                           {
                                               LocationId = bb.Bed.LocationId,
                                               Name = bb.Bed.Location.Name,
                                               SiteId = bb.Bed.Location.SiteId,
                                               ResidentMedicalOfficer = bb.Bed.Location.ResidentMedicalOfficerId.HasValue
                                                    ? new PersonDTO
                                                    {
                                                        PersonId = bb.Bed.Location.ResidentMedicalOfficer.PersonId,
                                                        Id = bb.Bed.Location.ResidentMedicalOfficer.ClinicianId,
                                                        FullnameReverse = bb.Bed.Location.ResidentMedicalOfficer.Person.FullnameReverse
                                                    }
                                                    : null
                                           }
                                       })
                                       .FirstOrDefault(),
            ObservationDue = ip.ObservationDue,
            ObservationFrequencyInMinutes = ip.ObservationFrequencyInMinutes,
            Patient = new PatientDTO
            {
                PatientId = ip.CommonBooking.Patient.PatientId,
                DateOfBirth = ip.CommonBooking.Patient.DateOfBirth,
                Gender = ip.CommonBooking.Patient.Gender,
                FullnameReverse = ip.CommonBooking.Patient.FullnameReverse,
                IsDeceased = ip.CommonBooking.Patient.IsDeceased,
                HasAlerts = ip.CommonBooking.Patient.Alerts.Any(a => !a.DateExpired.HasValue || DateTime.Today > a.DateExpired)
            },
            NEWSCurrent = ip.CommonBooking.FormResults.OrderByDescending(fr => fr.CreateDate)
                                                          .SelectMany(fr => fr.FormComponentResults.Where(fcr => fcr.FormComponent.SystemTypeId == FormResultsController.NEWSSystemTypeId)
                                                                                                   .Select(fcr => new FormResultValue { Result = fcr.Result, WarningColour = fcr.WarningColour }))
                                                          .FirstOrDefault(),
            NEWSPrevious = ip.CommonBooking.FormResults.OrderByDescending(fr => fr.CreateDate)
                                                           .SelectMany(fr => fr.FormComponentResults.Where(fcr => fcr.FormComponent.SystemTypeId == FormResultsController.NEWSSystemTypeId)
                                                                                                    .Select(fcr => new FormResultValue { Result = fcr.Result, WarningColour = fcr.WarningColour }))
                                                           .Skip(1)
                                                           .FirstOrDefault()
        };
    }
}