using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelConfiguration
{
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.StartedOn)
                .IsRequired()
                .HasColumnType("datetime2(7)");
            
            builder.Property(a => a.EndedOn)
                .IsRequired()
                .HasColumnType("datetime2(7)");

            builder.HasMany(a => a.Items)
                .WithOne(s => s.Auction)
                .HasForeignKey(s => s.AuctionId)
                .IsRequired(false);

            builder.Ignore(a => a.Bids);
            builder.Ignore(a => a.Categories);
        }
    }
}