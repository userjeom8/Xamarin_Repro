using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompucareWard.API.Infrastructure;
using CompucareWard.API.Models;
using IdentityModel;
using CompucareWard.API.DTOs;
using CompucareWard.API.Services;

namespace CompucareWard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly CompucareWardContext _context;
        private readonly IGlobalSettingsService _globalSettingsService;

        public PeopleController(CompucareWardContext context, IGlobalSettingsService globalSettingsService)
        {
            _context = context;
            _globalSettingsService = globalSettingsService;
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPersons()
        {
            return _context.Persons;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.Persons.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (int.TryParse(User.FindFirst(JwtClaimTypes.Subject).Value, out var userId))
            {
                var user = await _context.Users
                                     .GroupJoin(_context.Clinicians, u => u.PersonId, c => c.PersonId, (u, c) => new { User = u, Clinicians = c })
                                        .SelectMany(c => c.Clinicians.DefaultIfEmpty(), (u, c) => new { u.User, c.ClinicianId })
                                     .GroupJoin(_context.Nurses, u => u.User.PersonId, n => n.PersonId, (uc, n) => new { uc.User, uc.ClinicianId, Nurses = n })
                                        .SelectMany(c => c.Nurses.DefaultIfEmpty(), (u, c) => new { u.User, u.ClinicianId, c.NurseId })
                                     .Select(u => new UserDTO()
                                     {
                                         ClinicianId = u.ClinicianId,
                                         UserId = u.User.UserId,
                                         PersonId = u.User.PersonId,
                                         Location = u.User.Location != null ? new LocationDTO
                                         {
                                             LocationId = u.User.LocationId.Value,
                                             Name = u.User.Location.Name,
                                             SiteId = u.User.Location.SiteId,
                                             ResidentMedicalOfficer = u.User.Location.ResidentMedicalOfficer != null
                                                ? new PersonDTO
                                                {
                                                    Id = u.User.Location.ResidentMedicalOfficerId.Value,
                                                    PersonId = u.User.Location.ResidentMedicalOfficer.PersonId,
                                                    FullnameReverse = u.User.Location.ResidentMedicalOfficer.Person.FullnameReverse
                                                } : null,
                                         } : null,

                                         FullnameReverse = u.User.Person.FullnameReverse,
                                         NurseId = u.NurseId,
                                         Thumbnail = u.User.Person.Thumbnail
                                     })
                                     .FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null)
                    return NotFound();

                var (licenceNumber, systemType) = await _globalSettingsService.GetSystemInfo();
                user.LicenceNumber = licenceNumber;
                user.SystemType = systemType;
                return Ok(user);
            }

            return NotFound();
        }

        [HttpGet()]
        [Route("Contacts")]
        public async Task<IList<ContactDTO>> GetContactDetails([FromQuery(Name = "personIds")] int[] personIds)
        {
            var contactInfos = await _context.Persons.Where(ip => personIds.Contains(ip.PersonId)).ToListAsync();

            var contacts = new List<ContactDTO>();

            foreach (var contactInfo in contactInfos)
            {
                if (!string.IsNullOrEmpty(contactInfo?.MobilePhone))
                {
                    contacts.Add(new ContactDTO
                    {
                        PersonId = contactInfo.PersonId,
                        FullnameReverse = contactInfo.FullnameReverse,
                        Type = nameof(Person.MobilePhone),
                        Value = contactInfo.MobilePhone
                    });
                }

                if (!string.IsNullOrEmpty(contactInfo?.MainPhone))
                {
                    var isPager = contactInfo.MainPhone.StartsWith("000");

                    contacts.Add(new ContactDTO
                    {
                        PersonId = contactInfo.PersonId,
                        FullnameReverse = contactInfo.FullnameReverse,                        
                        Type = isPager? "Pager" : nameof(Person.MainPhone),
                        Value = isPager ? contactInfo.MainPhone.TrimStart(new char[] { '0' }) : contactInfo.MainPhone
                    });
                }

                if (!string.IsNullOrEmpty(contactInfo?.WorkPhone))
                {
                    contacts.Add(new ContactDTO
                    {
                        PersonId = contactInfo.PersonId,
                        FullnameReverse = contactInfo.FullnameReverse,
                        Type = nameof(Person.WorkPhone),
                        Value = contactInfo.WorkPhone
                    });
                }

                if (!string.IsNullOrEmpty(contactInfo?.Email))
                {
                    contacts.Add(new ContactDTO
                    {
                        PersonId = contactInfo.PersonId,
                        FullnameReverse = contactInfo.FullnameReverse,
                        Type = nameof(Person.Email),
                        IsEmail = true,
                        Value = contactInfo.Email
                    });
                }
            }
            return contacts;
        }
    }
}