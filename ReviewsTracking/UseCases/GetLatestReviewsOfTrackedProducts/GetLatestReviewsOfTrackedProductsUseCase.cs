using Core;
using Core.ReviewsCollection.Entities;
using Core.ReviewsTracking.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ReviewsTracking.UseCases.GetLatestReviewsOfTrackedProducts
{
    public class GetLatestReviewsOfTrackedProductsUseCase : IGetLatestReviewsOfTrackedProductsUseCase
    {
        private readonly ReviewsTrackingContext _reviewsTrackingContext;

        public GetLatestReviewsOfTrackedProductsUseCase(
            ReviewsTrackingContext reviewsTrackingContext)
        {
            _reviewsTrackingContext = reviewsTrackingContext;
        }

        public IEnumerable<ProductReview> GetLatestReviews(int nbElements)
        {
            // Récupération de la liste des produits à suivre.
            var productIds = _reviewsTrackingContext.ProductTrackings
                .Select(t => t.IdProduct)
                .ToList();

            var productsReviews = _reviewsTrackingContext.ProductReviews
                .Where(pr => productIds.Contains(pr.IdProduct))
                .AsEnumerable()
                .GroupBy(pr => pr.IdProduct)
                .Select(pr => new
                {
                    pr,
                    Count = pr.Count()
                })
                .SelectMany(prWithCount =>
                    prWithCount.pr.OrderByDescending(pr => pr.DateAndPlace.ToDateTimeFromDateAndPlaceString())
                        .Select(p => p)
                        .Zip(
                            Enumerable.Range(1, prWithCount.Count),
                            (pr, rn) => new { ProdctReview = pr, RowNumber = rn }))
                .Where(pr => pr.RowNumber <= nbElements)
                .Select(pr => pr.ProdctReview)
                .OrderByDescending(pr => pr.DateAndPlace.ToDateTimeFromDateAndPlaceString())
                .ToList();

            return productsReviews;
        }
    }
}