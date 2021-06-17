using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelConfiguration
{
    public class TraderConfiguration : IEntityTypeConfiguration<Trader>
    {
        public void Configure(EntityTypeBuilder<Trader> builder)
        {
            builder.ToTable("Traders");

            builder.HasKey(t => t.IdentityGuid);
            
            builder.HasMany(t => t.CreatedSlots)
                .WithOne(s => s.Owner)
                .HasForeignKey(s => s.OwnerId) // or it can be set to TraderId by EF Core
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); //delete all slots referenced by user being removed
            
            builder.HasMany(t => t.Bids)
                .WithOne(b => b.Trader)
                .HasForeignKey(s => s.TraderId) // or it can be set to TraderId by EF Core
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); //delete all slots referenced by user being removed
            
            builder.Ignore(t => t.CreatedAuctions);
        }
    }
}