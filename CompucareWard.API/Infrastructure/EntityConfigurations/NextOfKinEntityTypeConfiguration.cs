using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class NextOfKinEntityTypeConfiguration : IEntityTypeConfiguration<NextOfKin>
    {
        public void Configure(EntityTypeBuilder<NextOfKin> builder)
        {
            builder.ToTable("NextOfKins");

            builder.HasKey(ci => ci.NextOfKinId);

            builder.Property(ci => ci.NextOfKinId).IsRequired();
        }
    }
}
