using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;

namespace Nop.Plugin.Company.Media.Mapping
{    
    public partial class MediaCategoriesMap : NopEntityTypeConfiguration<Domain.MediaCategories>
    {
        public override void Configure(EntityTypeBuilder<Domain.MediaCategories> builder)
        {
            builder.ToTable(nameof(Domain.MediaCategories));
            builder.HasKey(Mediacategory => Mediacategory.Id);
            builder.HasOne(x => x.Media).WithMany(x => x.MediaCategories).HasForeignKey(x => x.MediaId);
            builder.HasOne(x => x.MediaCategory).WithMany(x => x.MediaCategories).HasForeignKey(x => x.MediaCategoryId);
            base.Configure(builder);

        }
    }
}
