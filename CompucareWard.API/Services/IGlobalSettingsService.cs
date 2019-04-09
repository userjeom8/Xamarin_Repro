using CompucareWard.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Services
{
    public interface IGlobalSettingsService
    {
        Task<PatientSettingsDTO> GetPatientSettings();
        Task<NEWSSettingsDTO> GetNEWSSettings();
        InpatientBookingDTO ApplyStatus(PatientSettingsDTO patientSettings, InpatientBookingDTO inpatientBooking);
        Task<(string LicenceNumber, string SystemType)> GetSystemInfo();
    }
}
