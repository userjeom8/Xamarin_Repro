using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public interface INEWSService
    {
        Task<FormResult> GetNewFormResult(int patientId, int bookingId, int episodeOfCareId, string token);
        Task SaveItemAsync(FormResult item, string token);
        Task<ObservableCollection<FormResultSimplified>> GetNEWSScoresForBooking(int patientId, string token);
    }
}