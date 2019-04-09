using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class FormSubcomponentEntityTypeConfiguration : IEntityTypeConfiguration<FormSubcomponent>
    {
        public void Configure(EntityTypeBuilder<FormSubcomponent> builder)
        {
            builder.ToTable("FormSubcomponents");

            builder.HasKey(ci => ci.FormSubcomponentId);

            builder.Property(ci => ci.FormSubcomponentId).IsRequired();
            //builder.HasOne(fsc => fsc.FormComponent).WithMany(fc => fc.ChildFormSubcomponents).HasForeignKey(fsc => fsc.FormComponentId).IsRequired();
            //builder.HasOne(fsc => fsc.ParentFormComponent).WithMany(fc => fc.ParentFormSubcomponents).HasForeignKey(fsc => fsc.ParentFormComponentId).IsRequired();
            //builder.HasOne(fsc => fsc.ParentFormComponent).WithMany().IsRequired();
        }
    }
}
