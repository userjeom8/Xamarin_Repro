using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class RelationshipEntityTypeConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder.ToTable("Relationships");

            builder.HasKey(ci => ci.RelationshipId);

            builder.Property(ci => ci.RelationshipId).IsRequired();
        }
    }
}
