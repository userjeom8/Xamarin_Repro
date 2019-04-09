using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class NurseEntityTypeConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.ToTable("Nurses");

            builder.HasKey(ci => ci.NurseId);

            builder.Property(ci => ci.NurseId).IsRequired();
        }
    }
}
