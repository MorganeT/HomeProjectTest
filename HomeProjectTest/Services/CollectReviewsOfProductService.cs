using Core.ReviewsCollection.Entities;
using Core.ReviewsCollection.Services;
using Core.ReviewsTracking.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReviewsCollection.Services
{
    public class CollectReviewsOfProductService : ICollectReviewsOfProductService
    {
        private readonly IHtmlService _htmlService;
        private readonly IProductReviewService _productReviewService;
        private readonly INotificationService _notificationService;

        public CollectReviewsOfProductService(
            IHtmlService htmlService,
            IProductReviewService productReviewService,
            INotificationService notificationService)
        {
            _htmlService = htmlService;
            _productReviewService = productReviewService;
            _notificationService = notificationService;
        }


        public void CollectRecentsFromProduct(string asin)
        {
            var urlPattern = "https://www.amazon.com/product-reviews/{0}/?sortBy=recent&pageNumber={1}";
            var idPage = 1;
            var productReviews = new List<ProductReview>();
            var nodesEmpty = false;

            while (!nodesEmpty)
            {
                string urlOfReviewsPageOfProduct = string.Format(urlPattern, asin, idPage);

                var htmlDocument = _htmlService.GetDocumentFromUrl(urlOfReviewsPageOfProduct);

                var reviewNodes = htmlDocument.DocumentNode
                    .SelectNodes("//div[@data-hook='review']");

                // On arrête de récupérer le contenu HTML des pages lorsqu'il n'y a plus d'avis disponible.
                if (reviewNodes == null)
                {
                    nodesEmpty = true;
                }
                else
                {
                    idPage++;

                    // Pour chaque noeud correspondant à un avis, on récupère les informations.
                    foreach (var reviewNode in reviewNodes)
                    {
                        var review = new ProductReview
                        {
                            Id = reviewNode.Attributes["id"].Value,
                            Rating = reviewNode.SelectSingleNode(".//i[@data-hook='review-star-rating']")?.SelectSingleNode(".//span")?.InnerHtml,
                            UserName = reviewNode.SelectSingleNode(".//span[@class='a-profile-name']")?.InnerHtml,
                            Title = reviewNode.SelectSingleNode(".//a[@data-hook='review-title']")?.SelectSingleNode(".//span")?.InnerHtml,
                            DateAndPlace = reviewNode.SelectSingleNode(".//span[@data-hook='review-date']")?.InnerHtml,
                            Description = reviewNode.SelectSingleNode(".//span[@data-hook='review-body']")?.SelectSingleNode(".//span")?.InnerHtml,
                            VerifiedPurchase = reviewNode.SelectSingleNode(".//span[@data-hook='avp-badge']")?.InnerHtml,
                            IdProduct = asin
                        };

                        productReviews.Add(review);
                    }
                }
            }

            var savedProducts = _productReviewService.SaveAllNew(productReviews);
            if (savedProducts.Any())
            {
                _notificationService.NotifyNewReviewsOnProduct(asin);
            }
        }
    }
}