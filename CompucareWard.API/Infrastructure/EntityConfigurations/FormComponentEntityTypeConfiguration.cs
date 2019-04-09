using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class FormComponentEntityTypeConfiguration : IEntityTypeConfiguration<FormComponent>
    {
        public void Configure(EntityTypeBuilder<FormComponent> builder)
        {
            builder.ToTable("FormComponents");

            builder.HasKey(ci => ci.FormComponentId);

            builder.Property(ci => ci.FormComponentId).IsRequired();
            builder.HasMany(fcr => fcr.FormSubcomponents).WithOne(fcr => fcr.ParentFormComponent).HasForeignKey(fr => fr.ParentFormComponentId);
        }
    }
}
