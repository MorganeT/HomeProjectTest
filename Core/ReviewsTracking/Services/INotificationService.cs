namespace Core.ReviewsTracking.Services
{
    /// <summary>
    /// Service de notification.
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Alerte de la récupération de nouveaux avis sur un des produits suivis.
        /// </summary>
        /// <param name="idProduct">L'idenfitiant du produit pour lequel il y a des nouveaux avis.</param>
        void NotifyNewReviewsOnProduct(string idProduct);
    }
}
