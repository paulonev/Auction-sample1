using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelConfiguration
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.ToTable("Bids");
            
            builder.HasKey(b => b.Id);

            builder.Property(b => b.SlotId)
                .IsRequired();
            
            builder.Property(b => b.TraderId)
                .IsRequired();

            builder.Property(b => b.Date)
                .IsRequired()
                .HasColumnType("datetime2(7)");

            builder.Property(b => b.Amount)
                .HasColumnType("decimal(16,3)")
                .IsRequired();
        }
    }
}