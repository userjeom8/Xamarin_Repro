
using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class InpatientBookingEntityTypeConfiguration : IEntityTypeConfiguration<InpatientBooking>
    {
        public void Configure(EntityTypeBuilder<InpatientBooking> builder)
        {
            builder.ToTable("InpatientBookings");

            builder.HasKey(ci => ci.InpatientBookingId);

            builder.Property(ci => ci.InpatientBookingId).IsRequired();

            builder.HasOne(ib => ib.CommonBooking).WithOne();
        }
    }
}
