using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(i => i.InventoryId);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.LastDateProductAdded)
                .IsRequired();

            // Many-to-many relationship with Products through ProductInventory
            builder.HasMany(i => i.ProductInventories)
                .WithOne(pi => pi.Inventory)
                .HasForeignKey(pi => pi.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
