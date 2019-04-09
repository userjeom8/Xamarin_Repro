using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public interface IPatientService
    {
        Task<NextOfKin> GetPatientsPrimaryNextOfKin(int patientId, string token);
        Task<ObservableCollection<Alert>> GetPatientAlerts(int patientId, string token);
    }
}
