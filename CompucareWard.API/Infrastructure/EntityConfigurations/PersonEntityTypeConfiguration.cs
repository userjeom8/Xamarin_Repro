using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(ci => ci.PersonId);

            builder.Property(ci => ci.PersonId).IsRequired();
            //builder.Property(ci => ci.ResponsibleNurse).IsRequired();
        }
    }
}
