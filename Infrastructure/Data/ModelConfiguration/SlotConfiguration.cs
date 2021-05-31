using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.ModelConfiguration
{
    public class SlotConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(1024);
            
            builder.Property(s => s.StartPrice)
                .IsRequired()
                .HasColumnType("decimal(16,3)");
            
            builder.HasMany(s => s.Bids)
                .WithOne(b => b.Slot)
                .HasForeignKey(b => b.SlotId)
                .IsRequired();
            
            builder
                .HasMany(b => b.Pictures)
                .WithOne(i => i.Item)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}