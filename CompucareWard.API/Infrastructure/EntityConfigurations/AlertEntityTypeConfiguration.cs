using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class AlertEntityTypeConfiguration : IEntityTypeConfiguration<Alert>
    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            builder.ToTable("Alerts");

            builder.HasKey(ci => ci.AlertId);

            builder.Property(ci => ci.AlertReasonId).IsRequired();
        }
    }
}
