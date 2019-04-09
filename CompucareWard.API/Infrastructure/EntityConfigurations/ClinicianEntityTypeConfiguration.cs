using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class ClinicianEntityTypeConfiguration : IEntityTypeConfiguration<Clinician>
    {
        public void Configure(EntityTypeBuilder<Clinician> builder)
        {
            builder.ToTable("Clinicians");

            builder.HasKey(ci => ci.ClinicianId);

            builder.Property(ci => ci.ClinicianId).IsRequired();
        }
    }
}
