using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class FormResultEntityTypeConfigurationConfiguration : IEntityTypeConfiguration<FormResult>
    {
        public void Configure(EntityTypeBuilder<FormResult> builder)
        {
            builder.ToTable("FormResults");

            builder.HasKey(ci => ci.FormResultId);

            builder.Property(ci => ci.FormResultId).IsRequired();
            builder.Property(fr => fr.RowVersion).IsRowVersion();
        }
    }
}
