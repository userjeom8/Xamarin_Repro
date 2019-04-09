using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(ci => ci.PatientId);

            builder.Property(ci => ci.PatientId).IsRequired();
            //builder.Property(ci => ci.ResponsibleNurse).IsRequired();
        }
    }
}
