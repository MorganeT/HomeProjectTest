namespace Core.ReviewsCollection.Services
{
    /// <summary>
    /// Service de collecte des avis des produits.
    /// </summary>
    public interface ICollectReviewsOfProductService
    {
        /// <summary>
        /// Collecte les avis récents d'un produit à partir de son identifiant.
        /// </summary>
        /// <param name="asin">L'idenfitiant ASIN du produit.</param>
        void CollectRecentsFromProduct(string asin);
    }
}