
using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class AlertReasonEntityTypeConfiguration : IEntityTypeConfiguration<AlertReason>
    {
        public void Configure(EntityTypeBuilder<AlertReason> builder)
        {
            builder.ToTable("AlertReasons");

            builder.HasKey(ci => ci.AlertReasonId);
        }
    }
}
