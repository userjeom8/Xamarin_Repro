using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations");

            builder.HasKey(ci => ci.LocationId);
            builder.Property(ci => ci.LocationId).IsRequired();
            builder.Property(ci => ci.ResidentMedicalOfficerId).HasColumnName("ResidentMedicalOfficer");
        }
    }
}
