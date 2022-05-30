using Core.ReviewsCollection.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.ReviewsCollection.Models.Configurations
{
    public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .Property(p => p.IdProduct)
                .IsRequired();
        }
    }
}
