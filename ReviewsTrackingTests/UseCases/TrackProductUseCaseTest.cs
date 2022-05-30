using Core;
using Core.ReviewsTracking.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ReviewsTracking.UseCases.TrackProduct;
using System.Collections.Generic;
using System.Linq;

namespace ReviewsTrackingTests.UseCases
{
    [TestFixture]
    internal class TrackProductUseCaseTest
    {
        private TrackProductUseCase _useCase;
        private Mock<ReviewsTrackingContext> _mockDbContext;
        private IQueryable<ProductTracking> _data;

        [SetUp]
        public void SetUp()
        {
            _data = new List<ProductTracking>
            {
                new ProductTracking { IdProduct = "1" },
                new ProductTracking { IdProduct = "2" }
            }.AsQueryable();

            var dbSet = new Mock<DbSet<ProductTracking>>();
            dbSet.As<IQueryable<ProductTracking>>().Setup(m => m.Provider).Returns(_data.Provider);
            dbSet.As<IQueryable<ProductTracking>>().Setup(m => m.Expression).Returns(_data.Expression);
            dbSet.As<IQueryable<ProductTracking>>().Setup(m => m.ElementType).Returns(_data.ElementType);
            dbSet.As<IQueryable<ProductTracking>>().Setup(m => m.GetEnumerator()).Returns(_data.GetEnumerator());

            _mockDbContext = new Mock<ReviewsTrackingContext>();
            _mockDbContext
                .SetupGet(s => s.ProductTrackings)
                .Returns(dbSet.Object);

            _useCase = new TrackProductUseCase(_mockDbContext.Object);
        }

        [Test]
        public void TrackValide()
        {
            var request = new TrackProductRequest { IdProduct = "3" };

            _useCase.Track(request);
            
            Assert.Multiple(() =>
            {
                _mockDbContext.Verify(c => c.Add(It.Is<ProductTracking>(pt => pt.IdProduct == request.IdProduct)));
                _mockDbContext.Verify(c => c.SaveChanges());
            });
        }

        [Test]
        public void Track_AlreadySaved()
        {
            var request = new TrackProductRequest { IdProduct = "1" };

            _useCase.Track(request);

            Assert.Multiple(() =>
            {
                _mockDbContext.Verify(c => c.Add(It.IsAny<ProductTracking>()), Times.Never);
                _mockDbContext.Verify(c => c.SaveChanges(), Times.Never);
            });
        }
    }
}
