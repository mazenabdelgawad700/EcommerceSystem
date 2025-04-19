using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class ProductSellerConfiguration : IEntityTypeConfiguration<ProductSeller>
    {
        public void Configure(EntityTypeBuilder<ProductSeller> builder)
        {
            // Composite primary key
            builder.HasKey(ps => new { ps.UserId, ps.ProductId });

            // Many-to-one relationship with User
            builder.HasOne(ps => ps.User)
                .WithMany(u => u.ProductSellers)
                .HasForeignKey(ps => ps.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one relationship with Product
            builder.HasOne(ps => ps.Product)
                .WithOne(p => p.ProductSeller)
                .HasForeignKey<ProductSeller>(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
