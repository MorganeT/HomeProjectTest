using Core.ReviewsTracking.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.ReviewsTracking.Models.Configurations
{
    public class ProductTrackingConfiguration : IEntityTypeConfiguration<ProductTracking>
    {
        public void Configure(EntityTypeBuilder<ProductTracking> builder)
        {
            builder
                .HasKey(p => p.IdProduct);
        }
    }
}
