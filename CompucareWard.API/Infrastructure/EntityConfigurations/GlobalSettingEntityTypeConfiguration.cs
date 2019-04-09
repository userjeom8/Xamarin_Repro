using CompucareWard.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompucareWard.API.Infrastructure.EntityConfigurations
{
    class GlobalSettingEntityTypeConfiguration : IEntityTypeConfiguration<GlobalSetting>
    {
        public void Configure(EntityTypeBuilder<GlobalSetting> builder)
        {
            builder.ToTable("GlobalSettings");

            builder.HasKey(ci => ci.GlobalSettingID);
            builder.Property(ci => ci.Settings).IsRequired();
        }
    }
}
