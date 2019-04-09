using CompucareWard.API.Infrastructure;
using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Services
{
    public class InpatientBookingServiceInternal : IInpatientBookingServiceInternal
    {
        private readonly CompucareWardContext _context;

        public InpatientBookingServiceInternal(CompucareWardContext context)
        {
            _context = context;
        }

        async Task IInpatientBookingServiceInternal.UpdateObservationFrequency(int inpatientBookingId, int frequencyInMinutes)
        {
            var booking = await _context.InpatientBookings.Where(ip => ip.InpatientBookingId == inpatientBookingId).Select(ip => new InpatientBooking()
            {
                InpatientBookingId = ip.InpatientBookingId,
                ObservationFrequencyInMinutes = ip.ObservationFrequencyInMinutes,
                ObservationDue = ip.ObservationDue
            }).FirstOrDefaultAsync();

            booking.ObservationFrequencyInMinutes = frequencyInMinutes;

            if (DateTime.Now.AddMinutes(frequencyInMinutes) is DateTime newDateTime && (!booking.ObservationDue.HasValue || newDateTime < booking.ObservationDue))
                booking.ObservationDue = newDateTime;

            _context.Entry(booking).Property(b => b.ObservationDue).IsModified = true;
            _context.Entry(booking).Property(b => b.ObservationFrequencyInMinutes).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
