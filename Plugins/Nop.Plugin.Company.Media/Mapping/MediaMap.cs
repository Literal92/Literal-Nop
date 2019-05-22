using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nop.Data.Mapping;

namespace Nop.Plugin.Company.Media.Mapping
{
    public partial class MediaMap : NopEntityTypeConfiguration<Domain.Media>
    {
        public override void Configure(EntityTypeBuilder<Domain.Media> builder)
        {
            builder.ToTable(nameof(Domain.Media));
            builder.HasKey(media => media.Id);
            builder.Property(media => media.Title);
            builder.Property(media => media.AltText);
            builder.Property(media => media.URL);
            builder.Property(media => media.UserId);
            builder.Property(media => media.ImageThumb);
            builder.Property(media => media.FileName);
            builder.Property(media => media.CreateDate);
            builder.Property(media => media.PublishDate);
            builder.Property(media => media.UpdateDate);
            builder.Property(media => media.ContentType);
            builder.Property(media => media.MediaStatus);
            builder.Property(media => media.Viewer);
            builder.Property(media => media.DownloadNumber);            
            base.Configure(builder);

        }
    }
}
