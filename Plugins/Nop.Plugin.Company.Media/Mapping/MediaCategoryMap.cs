using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;

namespace Nop.Plugin.Company.Media.Mapping
{   
    public partial class MediaCategoryMap : NopEntityTypeConfiguration<Domain.MediaCategory>
    {
        public override void Configure(EntityTypeBuilder<Domain.MediaCategory> builder)
        {
            builder.ToTable(nameof(Domain.MediaCategory));
            builder.HasKey(Mediacategory => Mediacategory.Id);
            builder.Property(Mediacategory => Mediacategory.MediaCategoryStatus);
            builder.Property(Mediacategory => Mediacategory.MediaCategoryName);
            builder.Property(Mediacategory => Mediacategory.MediaCategoryLatinName);
            builder.Property(Mediacategory => Mediacategory.MediaCategoryCode);
            builder.Property(Mediacategory => Mediacategory.MediaCategoryLevel);
            builder.Property(Mediacategory => Mediacategory.MediaCategoryParentId);
            builder.Property(Mediacategory => Mediacategory.MediaCategoryMediaId);
            base.Configure(builder);

        }
    }
}
