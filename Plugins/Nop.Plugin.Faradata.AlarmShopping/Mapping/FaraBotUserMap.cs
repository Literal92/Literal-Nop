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
    public partial class FaraBotUserMap : NopEntityTypeConfiguration<FaraBotUser>
    {
        public override void Configure(EntityTypeBuilder<FaraBotUser> builder)
        {
            builder.ToTable(nameof(FaraBotUser));
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CustomerId);
            builder.Property(m => m.ChatId);
            builder.Property(m => m.CreateDate);
            builder.Property(m => m.IsActive);
            base.Configure(builder);
        }
    }
}
