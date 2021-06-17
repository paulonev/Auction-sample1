using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelConfiguration
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PictureUri)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(150)
                .IsRequired();
            
            builder.Property(p => p.ItemId)
                .IsRequired();
        }
    }
}