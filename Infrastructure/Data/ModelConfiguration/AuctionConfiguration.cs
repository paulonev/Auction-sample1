using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelConfiguration
{
    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Auction.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.StartedOn)
                .IsRequired()
                .HasColumnType("datetime2(7)");
            
            builder.Property(a => a.EndedOn)
                .IsRequired()
                .HasColumnType("datetime2(7)");

            builder.HasMany(a => a.Items)
                .WithOne(s => s.Auction)
                .HasForeignKey(s => s.AuctionId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.Ignore(a => a.Bids); //because bid depends on slot, not auction
            builder.Ignore(a => a.Categories); //because category is independent of auction
        }
    }
}