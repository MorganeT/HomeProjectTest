using Core.ReviewsCollection.Entities;
using System.Collections.Generic;

namespace ReviewsTracking.UseCases.GetLatestReviewsOfTrackedProducts
{
    /// <summary>
    /// Use case de récupération des derniers avis concernant les produits suivis.
    /// </summary>
    public interface IGetLatestReviewsOfTrackedProductsUseCase
    {
        /// <summary>
        /// Retourne la liste des derniers avis conernant les produits suivis.
        /// </summary>
        /// <param name="nbElements">Nombre d'avis les plus récents à récupérer par produit.</param>
        /// <returns>La liste des avis.</returns>
        IEnumerable<ProductReview> GetLatestReviews(int nbElements);
    }
}