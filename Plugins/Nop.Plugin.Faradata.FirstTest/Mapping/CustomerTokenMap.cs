using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Core;
using Nop.Data;
using Nop.Data.Mapping;
using Nop.Plugin.Faradata.Test.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nop.Plugin.Faradata.Test.Mapping
{
    public partial class CustomerTokenMap : NopEntityTypeConfiguration<CustomerToken>
    {
        public override void Configure(EntityTypeBuilder<CustomerToken> builder)
        {
            builder.ToTable(nameof(CustomerToken));
            builder.HasKey(m => m.Id);
            builder.Property(m => m.CustomerId);
            builder.Property(m => m.DeviceId);
            builder.Property(m => m.CreateDate);
            builder.Property(m => m.ExpireDate);
            builder.Property(m => m.Token);
            base.Configure(builder);
        }
    }
}
