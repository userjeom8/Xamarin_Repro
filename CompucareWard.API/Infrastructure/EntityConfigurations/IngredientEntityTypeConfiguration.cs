using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class IngredientEntityTypeConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ss1033_IngredientsExtension");

            builder.HasKey(ci => ci.IngredientId);
            builder.Property(i => i.IngredientId).HasColumnName("SRID");
            builder.Property(ci => ci.Name).IsRequired();
        }
    }
}
