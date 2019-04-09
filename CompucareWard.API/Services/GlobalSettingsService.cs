
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CompucareWard.API.Infrastructure;
using CompucareWard.API.DTOs;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System;
using CompucareWard.API.Models;

namespace CompucareWard.API.Services
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly CompucareWardContext _context;

        public GlobalSettingsService(CompucareWardContext context)
        {
            _context = context;
        }

        async Task<PatientSettingsDTO> IGlobalSettingsService.GetPatientSettings()
        {
            if (await _context.GlobalSettings.FirstOrDefaultAsync(u => u.GlobalSettingID == "PATIENT") is GlobalSetting patientSettings)
            {
                var xml = XDocument.Parse(patientSettings.Settings);

                return GetPatientSettings(
                    xml.Root.Element(nameof(PatientSettingsDTO.AgeAtWhichCeasesToBeChild))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusUnknownColour))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusChildUnknownColour))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusChildFemaleColour))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusFemaleColour))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusChildMaleColour))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusMaleColour))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusDeceasedColour))?.Value,
                    xml.Root.Element(nameof(PatientSettingsDTO.StatusNewColour))?.Value);
            }

            return GetPatientSettings();
        }

        async Task<(string LicenceNumber, string SystemType)> IGlobalSettingsService.GetSystemInfo()
        {
            string licence = null;
            string systemType = null;

            if (await _context.GlobalSettings.Where(u => u.GlobalSettingID == "LICENCE" || u.GlobalSettingID == "SYSTEM").ToListAsync() is IList<GlobalSetting> settings)
            {
                if (settings.FirstOrDefault(s => s.GlobalSettingID == "LICENCE") is GlobalSetting licenceSettings)
                    licence = XDocument.Parse(licenceSettings.Settings).Root.Element("licenceNumber").Value;

                if (settings.FirstOrDefault(s => s.GlobalSettingID == "SYSTEM") is GlobalSetting systemSettings 
                    && XDocument.Parse(systemSettings.Settings).Root.Element("SystemType").Value is string value && !string.IsNullOrEmpty(value))
                    systemType = value;
                else
                    systemType = "Live";
            }

            return (licence, systemType);
        }

        async Task<NEWSSettingsDTO> IGlobalSettingsService.GetNEWSSettings()
        {
            var newsSettings = new NEWSSettingsDTO();

            if (await _context.GlobalSettings.FirstOrDefaultAsync(u => u.GlobalSettingID == "SCHEDULING") is GlobalSetting schedulingSettings)
            {
                var xml = XDocument.Parse(schedulingSettings.Settings);
                newsSettings.GuidanceDocumentURL = xml.Root.Element(nameof(NEWSSettingsDTO.GuidanceDocumentURL))?.Value;
                newsSettings.NEWSChartThresholdsTriggers = xml.Root.Descendants().Elements("NEWSChartThresholdTrigger")
                                                              .Select(nt => new NEWSChartThresholdTrigger()
                                                              {
                                                                  Has3InSingleResult = Convert.ToBoolean(nt.Element(nameof(NEWSChartThresholdTrigger.Has3InSingleResult)).Value),
                                                                  HighLimit = Convert.ToInt32(nt.Element(nameof(NEWSChartThresholdTrigger.HighLimit)).Value),
                                                                  LowLimit = Convert.ToInt32(nt.Element(nameof(NEWSChartThresholdTrigger.LowLimit)).Value),
                                                                  ObservationFrequencyInMinutes = Convert.ToInt32(nt.Element(nameof(NEWSChartThresholdTrigger.ObservationFrequencyInMinutes)).Value),
                                                                  ResponseText = nt.Element(nameof(NEWSChartThresholdTrigger.ResponseText)).Value
                                                              })
                                                              ?.ToList() ?? new List<NEWSChartThresholdTrigger>();
            }

            return newsSettings;
        }

        InpatientBookingDTO IGlobalSettingsService.ApplyStatus(PatientSettingsDTO patientSettings, InpatientBookingDTO inpatientBooking)
        {
            string child = "U";

            if (inpatientBooking.Patient.DateOfBirth.HasValue)
                child = (new DateTime(DateTime.Now.Subtract(inpatientBooking.Patient.DateOfBirth.Value).Ticks).Year - 1) < patientSettings.AgeAtWhichCeasesToBeChild ? "C" : "A";

            string childGenderDeceased = inpatientBooking.Patient.IsDeceased ? "D" : child + inpatientBooking.Patient.Gender.ToString().Substring(0, 1);

            switch (childGenderDeceased)
            {
                case "C1":
                    inpatientBooking.Patient.Status = 2;
                    inpatientBooking.Patient.StatusColour = patientSettings.StatusChildMaleColour;
                    break;
                case "C2":
                    inpatientBooking.Patient.Status = 1;
                    inpatientBooking.Patient.StatusColour = patientSettings.StatusChildFemaleColour;
                    break;
                case "C0":
                case "C3":
                    inpatientBooking.Patient.Status = 3;
                    inpatientBooking.Patient.StatusColour = patientSettings.StatusChildUnknownColour;
                    break;
                case "U1":
                case "A1":
                    inpatientBooking.Patient.Status = 5;
                    inpatientBooking.Patient.StatusColour = patientSettings.StatusMaleColour;
                    break;
                case "U2":
                case "A2":
                    inpatientBooking.Patient.Status = 4;
                    inpatientBooking.Patient.StatusColour = patientSettings.StatusFemaleColour;
                    break;
                case "D":
                    inpatientBooking.Patient.Status = 6;
                    inpatientBooking.Patient.StatusColour = patientSettings.StatusChildMaleColour;
                    break;
                case "U0":
                case "A0":
                case "A3":
                default:
                    inpatientBooking.Patient.Status = 0;
                    inpatientBooking.Patient.StatusColour = patientSettings.StatusChildMaleColour;
                    break;
            }

            return inpatientBooking;
        }



        PatientSettingsDTO GetPatientSettings(string childAge = null, string unknownColour = null, string childUnknownColour = null, string childFemaleColour = null, string femaleColour = null, 
            string childMaleColour = null, string maleColour = null, string deceasedColour = null, string newColour = null)
        {
            return new PatientSettingsDTO()
            {
                AgeAtWhichCeasesToBeChild = int.Parse(childAge ?? "18"),
                StatusUnknownColour = unknownColour ?? "#A6D55E",
                StatusChildUnknownColour = childUnknownColour ?? "#A6D55E",
                StatusChildFemaleColour = childFemaleColour ?? "#BD87FE",
                StatusFemaleColour = femaleColour ?? "#BD87FE",
                StatusChildMaleColour = childMaleColour ?? "#51AEFB",
                StatusMaleColour = maleColour ?? "#51AEFB",
                StatusDeceasedColour = deceasedColour ?? "#000000",
                StatusNewColour = newColour ?? "#A4A8AA",
            };
        }
    }
}
