
using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class CommonBookingEntityTypeConfiguration : IEntityTypeConfiguration<CommonBooking>
    {
        public void Configure(EntityTypeBuilder<CommonBooking> builder)
        {
            builder.ToTable("CommonBookings");

            builder.HasKey(ci => ci.CommonBookingId);

            builder.Property(ci => ci.CommonBookingId).IsRequired();

            builder.HasOne(cb => cb.Patient).WithMany();
        }
    }
}
