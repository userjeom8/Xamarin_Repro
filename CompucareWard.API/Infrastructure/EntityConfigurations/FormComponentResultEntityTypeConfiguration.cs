using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class FormComponentResultEntityTypeConfiguration : IEntityTypeConfiguration<FormComponentResult>
    {
        public void Configure(EntityTypeBuilder<FormComponentResult> builder)
        {
            builder.ToTable("FormComponentResults");

            builder.HasKey(ci => ci.FormComponentResultId);

            builder.Property(ci => ci.FormComponentResultId).IsRequired();
            builder.HasMany(fcr => fcr.ChildFormComponentResults).WithOne(fcr => fcr.ParentFormComponentResult).HasForeignKey(fr => fr.ParentFormComponentResultId);
            builder.Property(fcr => fcr.RowVersion).IsRowVersion();
        }
    }
}
