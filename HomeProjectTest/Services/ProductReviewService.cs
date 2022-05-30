using Core;
using Core.ReviewsCollection.Entities;
using Core.ReviewsCollection.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReviewsCollection.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly ReviewsTrackingContext _reviewsContext;

        public ProductReviewService(
            ReviewsTrackingContext reviewsContext)
        {
            _reviewsContext = reviewsContext;
        }

        public IEnumerable<ProductReview> SaveAllNew(IEnumerable<ProductReview> productReviews)
        {
            // Récupération des avis déjà collectés
            var productReviewsDb = _reviewsContext.ProductReviews
                .Where(pr => productReviews.Select(npr => npr.Id).Contains(pr.Id))
                .ToList();

            // On conserve uniquement les avis qui n'ont pas encore été collectés.
            var newProductReviews = productReviews
                .Where(npr => !productReviewsDb.Any(pr => npr.Id == pr.Id));

            _reviewsContext.ProductReviews.AddRange(newProductReviews);
            _reviewsContext.SaveChanges();

            return newProductReviews;
        }
    }
}
