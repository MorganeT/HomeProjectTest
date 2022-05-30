using Core.ReviewsCollection.Services;
using Microsoft.AspNetCore.Mvc;
using ReviewsCollection.Requests;

namespace ReviewsCollection.Controllers
{
    /// <summary>
    /// Controleur pour la collecte des avis
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReviewsCollectionController : ControllerBase
    {
        public ReviewsCollectionController()
        {
        }

        /// <summary>
        /// Permet de collecter tous les avis non encore collectés d'un produit.
        /// </summary>
        [HttpPost]
        [Route("collect")]
        public void CollectReviewsOfProduct(
            [FromServices] ICollectReviewsOfProductService service,
            [FromBody] CollectReviewsOfProductRequest request)
        {
            service.CollectRecentsFromProduct(request.ProductIdentifier);
        }
    }
}