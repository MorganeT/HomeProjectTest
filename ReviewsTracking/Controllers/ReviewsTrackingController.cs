using Core.ReviewsCollection.Entities;
using Microsoft.AspNetCore.Mvc;
using ReviewsTracking.UseCases.GetLatestReviewsOfTrackedProducts;
using ReviewsTracking.UseCases.TrackProduct;
using System.Collections.Generic;

namespace ReviewsTracking.Controllers
{
    /// <summary>
    /// Controleur pour la gestion du suivi des avis.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReviewsTrackingController : ControllerBase
    {
        public ReviewsTrackingController()
        {
        }

        /// <summary>
        /// Récupère les derniers avis concernant les produits suivis.
        /// </summary>
        [HttpGet]
        [Route("latest-reviews")]
        public IEnumerable<ProductReview> GetLatestReviewsOfTrackedProducts(
            [FromServices] IGetLatestReviewsOfTrackedProductsUseCase useCase)
        {
            return useCase.GetLatestReviews(50);
        }

        /// <summary>
        /// Permet de suivre un nouveau produit.
        /// </summary>
        [HttpPost]
        [Route("track")]
        public void TrackProduct(
            [FromServices] ITrackProductUseCase useCase,
            [FromBody] TrackProductRequest request)
        {
            useCase.Track(request);
        }
    }
}