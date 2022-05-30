using Core;
using Core.ReviewsTracking.Services;
using System.Linq;

namespace ReviewsCollection.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ReviewsTrackingContext _reviewsTrackingContext;

        public NotificationService(
            ReviewsTrackingContext reviewsTrackingContext)
        {
            _reviewsTrackingContext = reviewsTrackingContext;
        }

        public void NotifyNewReviewsOnProduct(string idProduct)
        {
            if (_reviewsTrackingContext.ProductTrackings.Any(pt => pt.IdProduct == idProduct))
            {
                // Alerter des nouveaux avis
            }
        }
    }
}
