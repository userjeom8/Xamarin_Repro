using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class BedBookingEntityTypeConfiguration : IEntityTypeConfiguration<BedBooking>
    {
        public void Configure(EntityTypeBuilder<BedBooking> builder)
        {
            builder.ToTable("BedBookings");

            builder.HasKey(ci => ci.ScheduleId);

            builder.Property(ci => ci.ScheduleId).IsRequired();
        }
    }
}
