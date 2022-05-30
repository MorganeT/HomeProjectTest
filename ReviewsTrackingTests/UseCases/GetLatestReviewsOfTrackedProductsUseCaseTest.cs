using Core;
using Core.ReviewsCollection.Entities;
using Core.ReviewsTracking.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ReviewsTracking.UseCases.GetLatestReviewsOfTrackedProducts;
using System.Collections.Generic;
using System.Linq;

namespace ReviewsTrackingTests.UseCases
{
    [TestFixture]
    internal class GetLatestReviewsOfTrackedProductsUseCaseTest
    {
        private GetLatestReviewsOfTrackedProductsUseCase _useCase;
        private Mock<ReviewsTrackingContext> _mockDbContext;

        [SetUp]
        public void SetUp()
        {
            var productTrackings = new List<ProductTracking>
            {
                new ProductTracking { IdProduct = "1" },
                new ProductTracking { IdProduct = "2" }
            }.AsQueryable();

            var productReviews = new List<ProductReview>
            {
                new ProductReview 
                { 
                    Id = "1",
                    IdProduct = "1" ,
                    DateAndPlace = "Reviewed in the United States on March 22, 2020"
                },
                new ProductReview
                {
                    Id = "2",
                    IdProduct = "1",
                    DateAndPlace = "Reviewed in the United States on March 20, 2020"
                },
                new ProductReview
                {
                    Id = "3",
                    IdProduct = "1",
                    DateAndPlace = "Reviewed in the United States on March 21, 2020"
                },
                new ProductReview
                {
                    Id = "4",
                    IdProduct = "2",
                    DateAndPlace = "Reviewed in the United States on March 10, 2020"
                },
                new ProductReview
                {
                    Id = "5",
                    IdProduct = "3",
                    DateAndPlace = "Reviewed in the United States on March 29, 2020"
                }
            }.AsQueryable();

            var dbSetProductTracking = new Mock<DbSet<ProductTracking>>();
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.Provider).Returns(productTrackings.Provider);
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.Expression).Returns(productTrackings.Expression);
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.ElementType).Returns(productTrackings.ElementType);
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.GetEnumerator()).Returns(productTrackings.GetEnumerator());


            var dbSetProductReview = new Mock<DbSet<ProductReview>>();
            dbSetProductReview.As<IQueryable<ProductReview>>().Setup(m => m.Provider).Returns(productReviews.Provider);
            dbSetProductReview.As<IQueryable<ProductReview>>().Setup(m => m.Expression).Returns(productReviews.Expression);
            dbSetProductReview.As<IQueryable<ProductReview>>().Setup(m => m.ElementType).Returns(productReviews.ElementType);
            dbSetProductReview.As<IQueryable<ProductReview>>().Setup(m => m.GetEnumerator()).Returns(productReviews.GetEnumerator());

            _mockDbContext = new Mock<ReviewsTrackingContext>();
            _mockDbContext
                .SetupGet(s => s.ProductTrackings)
                .Returns(dbSetProductTracking.Object);
            _mockDbContext
                .SetupGet(s => s.ProductReviews)
                .Returns(dbSetProductReview.Object);

            _useCase = new GetLatestReviewsOfTrackedProductsUseCase(_mockDbContext.Object);
        }

        [Test]
        public void GetLatestReviews_Valide_AucunAvis()
        {
            var productTrackings = new List<ProductTracking>
            {
                new ProductTracking { IdProduct = "4" }
            }.AsQueryable();


            var dbSetProductTracking = new Mock<DbSet<ProductTracking>>();
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.Provider).Returns(productTrackings.Provider);
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.Expression).Returns(productTrackings.Expression);
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.ElementType).Returns(productTrackings.ElementType);
            dbSetProductTracking.As<IQueryable<ProductTracking>>().Setup(m => m.GetEnumerator()).Returns(productTrackings.GetEnumerator());

            _mockDbContext
                .SetupGet(s => s.ProductTrackings)
                .Returns(dbSetProductTracking.Object);

            var reviews = _useCase.GetLatestReviews(10);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(0, reviews.Count());
            });
        }

        [Test]
        public void GetLatestReviews_Valide()
        {
            var reviews = _useCase.GetLatestReviews(2);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(3, reviews.Count());

                Assert.AreEqual("1", reviews.ElementAt(0).Id);
                Assert.AreEqual("3", reviews.ElementAt(1).Id);
                Assert.AreEqual("4", reviews.ElementAt(2).Id);
            });
        }
    }
}
