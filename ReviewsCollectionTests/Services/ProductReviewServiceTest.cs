using Core;
using Core.ReviewsCollection.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ReviewsCollection.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReviewsCollectionTests.Services
{
    [TestFixture]
    internal class ProductReviewServiceTest
    {
        private ProductReviewService _service;
        private Mock<ReviewsTrackingContext> _mockDbContext;

        [SetUp]
        public void SetUp()
        {
            var data = new List<ProductReview>
            {
                new ProductReview { Id = "1" },
                new ProductReview { Id = "2" }
            }.AsQueryable();

            var dbSet = new Mock<DbSet<ProductReview>>();
            dbSet.As<IQueryable<ProductReview>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<ProductReview>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<ProductReview>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<ProductReview>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockDbContext = new Mock<ReviewsTrackingContext>();
            _mockDbContext
                .SetupGet(s => s.ProductReviews)
                .Returns(dbSet.Object);

            _service = new ProductReviewService(
                _mockDbContext.Object);

        }

        [Test]
        public void SaveAllNewValide()
        {
            var productReview = new ProductReview
            {
                Id = "2"
            };
            var productReviewMissing = new ProductReview
            {
                Id = "3"
            };

            _service.SaveAllNew(new List<ProductReview> { productReview, productReviewMissing });

            _mockDbContext.Verify(c => 
                c.ProductReviews.AddRange(It.Is<IEnumerable<ProductReview>>(l => 
                    l.Count() == 1 && l.Any(pr => pr.Id == productReviewMissing.Id))));
            _mockDbContext.Verify(c => c.SaveChanges());
        }
    }
}
