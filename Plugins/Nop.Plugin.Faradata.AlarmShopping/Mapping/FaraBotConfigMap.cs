using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core;
using Nop.Data;
using Nop.Data.Mapping;
using Nop.Plugin.Faradata.AlarmShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Faradata.AlarmShopping.Mapping
{
    public partial class FaraBotConfigMap : NopEntityTypeConfiguration<FaraBotConfig>
    {
        public override void Configure(EntityTypeBuilder<FaraBotConfig> builder)
        {
            builder.ToTable(nameof(FaraBotConfig));
            builder.HasKey(m => m.Id);
            builder.Property(m => m.TokenApi);
            builder.Property(m => m.IsActive);
            base.Configure(builder);
        }
    }
}
