using CompucareWard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CompucareWard.Services
{
    public interface IInpatientBookingService
    {
        Task SaveItemAsync(InpatientBooking item);
        Task<InpatientBooking> GetItemAsync(int id, string token);
        Task<ObservableCollection<InpatientBooking>> GetItemsByLocationAsync(int locationId, string token);
        Task<ObservableCollection<InpatientBooking>> GetMyPatients(int? responsibleNurseId, int? clinicianId, string token);
        Task<ObservableCollection<InpatientBooking>> GetItemsByRemindersAsync(int responsibleNurseId, string token);
        Task SaveNurseHandover(int handoverNurseId, int[] inpatientBookingsIds, string token);        
        Task SaveObservationFrequency(int inpatientBookingId, int frequencyInMinutes, string token);
    }
}
