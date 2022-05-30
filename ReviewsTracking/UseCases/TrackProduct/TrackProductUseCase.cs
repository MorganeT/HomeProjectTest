using Core;
using Core.ReviewsTracking.Entities;
using System.Linq;

namespace ReviewsTracking.UseCases.TrackProduct
{
    public class TrackProductUseCase : ITrackProductUseCase
    {
        private readonly ReviewsTrackingContext _reviewsContext;

        public TrackProductUseCase(ReviewsTrackingContext reviewsContext)
        {
            _reviewsContext = reviewsContext;
        }

        public void Track(TrackProductRequest request)
        {
            var tracking = new ProductTracking
            {
                IdProduct = request.IdProduct
            };

            if(!_reviewsContext.ProductTrackings.Any(pt => pt.IdProduct == tracking.IdProduct))
            {
                _reviewsContext.Add(tracking);
                _reviewsContext.SaveChanges();
            }
        }
    }
}