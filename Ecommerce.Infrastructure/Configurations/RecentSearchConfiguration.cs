using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations
{
    public class RecentSearchConfiguration : IEntityTypeConfiguration<RecentSearch>
    {
        public void Configure(EntityTypeBuilder<RecentSearch> builder)
        {
            // Primary key
            builder.HasKey(rs => new { rs.UserId, rs.SearchQuery });

            builder.Property(rs => rs.SearchQuery)
                .IsRequired()
                .HasMaxLength(255);

            // Many-to-one relationship with User
            builder.HasOne(rs => rs.User)
                .WithMany(u => u.RecentSearches)
                .HasForeignKey(rs => rs.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
