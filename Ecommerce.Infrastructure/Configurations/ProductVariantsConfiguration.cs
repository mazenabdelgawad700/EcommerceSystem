using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class ProductVariantsConfiguration : IEntityTypeConfiguration<ProductVariants>
    {
        public void Configure(EntityTypeBuilder<ProductVariants> builder)
        {
            builder.HasKey(pv => pv.Id);

            builder.Property(pv => pv.ProductId)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(pv => pv.Value)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pv => pv.VariantType)
                .IsRequired()
                .HasMaxLength(50);

            // Many-to-one relationship with Product
            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
