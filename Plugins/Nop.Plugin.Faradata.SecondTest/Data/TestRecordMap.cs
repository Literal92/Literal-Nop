using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.Test.Domain
{
    public class TestRecordMap : NopEntityTypeConfiguration<TestRecord>
    {
        public ProductViewTrackerRecordMap()
        {
            ToTable("ProductViewTracking");

            //Map the primary key
            HasKey(m => m.Id);
            //Map the additional properties
            Property(m => m.ProductId);
            //Avoiding truncation/failure 
            //so we set the same max length used in the product tame
            Property(m => m.ProductName).HasMaxLength(400);
            Property(m => m.IpAddress);
            Property(m => m.CustomerId);
            Property(m => m.IsRegistered);
        }
    }
}
