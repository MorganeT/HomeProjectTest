using Core.ReviewsCollection.Entities;
using System.Collections.Generic;

namespace Core.ReviewsCollection.Services
{
    /// <summary>
    /// Service de gestion des <see cref="ProductReview"/>
    /// </summary>
    public interface IProductReviewService
    {
        /// <summary>
        /// Sauvegarde tous les nouveaux avis.
        /// </summary>
        /// <param name="productReviews">Les avis des produits à sauvegarder.</param>
        /// <returns>La liste des avis ayant été sauvegardés.</returns>
        public IEnumerable<ProductReview> SaveAllNew(IEnumerable<ProductReview> productReviews);
    }
}
