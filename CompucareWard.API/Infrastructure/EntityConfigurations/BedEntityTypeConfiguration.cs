using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class BedEntityTypeConfiguration : IEntityTypeConfiguration<Bed>
    {
        public void Configure(EntityTypeBuilder<Bed> builder)
        {
            builder.ToTable("Beds");

            builder.HasKey(ci => ci.BedId);

            builder.Property(ci => ci.BedId).IsRequired();
        }
    }
}
