using Core.ReviewsCollection.Entities;
using Core.ReviewsCollection.Services;
using Core.ReviewsTracking.Services;
using HtmlAgilityPack;
using Moq;
using NUnit.Framework;
using ReviewsCollection.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReviewsCollectionTests.Services
{
    [TestFixture]
    internal class CollectReviewOfProductServiceTest
    {
        private CollectReviewsOfProductService _service;
        private Mock<IHtmlService> _mockHtmlService;
        private Mock<IProductReviewService> _mockProductReviewService;
        private Mock<INotificationService> _mockNotificationService;

        [SetUp]
        public void SetUp()
        {
            _mockHtmlService = new Mock<IHtmlService>();
            _mockProductReviewService = new Mock<IProductReviewService>();
            _mockNotificationService = new Mock<INotificationService>();

            _service = new CollectReviewsOfProductService(
                _mockHtmlService.Object,
                _mockProductReviewService.Object,
                _mockNotificationService.Object);
        }

        [Test]
        public void CollectRecentsFromProduct_AucunAvis()
        {
            _mockHtmlService
                .Setup(s => s.GetDocumentFromUrl(It.IsAny<string>()))
                .Returns(new HtmlDocument());

            var idProduct = "AUDIFUEIDN";

            _service.CollectRecentsFromProduct(idProduct);

            _mockHtmlService.Verify(s => s.GetDocumentFromUrl("https://www.amazon.com/product-reviews/AUDIFUEIDN/?sortBy=recent&pageNumber=1"));
            _mockHtmlService.Verify(s => s.GetDocumentFromUrl(It.Is<string>(s => s.StartsWith("https://www.amazon.com/product-reviews/AUDIFUEIDN"))), Times.Exactly(1));
            _mockProductReviewService.Verify(s => s.SaveAllNew(It.Is<List<ProductReview>>(l => l.Count == 0)));
            _mockNotificationService.Verify(s => s.NotifyNewReviewsOnProduct(It.IsAny<string>()), Times.Never);
        }


        [Test]
        public void CollectRecentsFromProduct_AucunNouvelAvis()
        {
            var title = "title";
            var rate = "1.0 out of 5 stars";
            var userName = "user";
            var description = "description";
            var verifiedPurchase = "Verified Purchase";
            var idProduct = "AUDIFUEIDN";
            var dateAndPlace = "Reviewed in the United States on March 8, 2020";
            var idReview = "1";

            HtmlDocument docComplet = new HtmlDocument();
            docComplet.LoadHtml(GetDocumentString(title, rate, idReview, userName, description, verifiedPurchase, dateAndPlace));

            _mockHtmlService
                .SetupSequence(s => s.GetDocumentFromUrl(It.IsAny<string>()))
                .Returns(docComplet)
                .Returns(new HtmlDocument());

            _mockProductReviewService
                .Setup(s => s.SaveAllNew(It.IsAny<List<ProductReview>>()))
                .Returns(new List<ProductReview>());


            _service.CollectRecentsFromProduct(idProduct);

            _mockHtmlService.Verify(s => s.GetDocumentFromUrl("https://www.amazon.com/product-reviews/AUDIFUEIDN/?sortBy=recent&pageNumber=1"));
            _mockHtmlService.Verify(s => s.GetDocumentFromUrl("https://www.amazon.com/product-reviews/AUDIFUEIDN/?sortBy=recent&pageNumber=2"));
            _mockHtmlService.Verify(s => s.GetDocumentFromUrl(It.Is<string>(s => s.StartsWith("https://www.amazon.com/product-reviews/AUDIFUEIDN"))), Times.Exactly(2));
            
            _mockProductReviewService.Verify(s => s.SaveAllNew(It.Is<List<ProductReview>>(l => 
                l.Count == 1
                && l.Any(pr => 
                    pr.Id == idReview
                    && pr.IdProduct == idProduct
                    && pr.DateAndPlace == dateAndPlace
                    && pr.VerifiedPurchase == verifiedPurchase
                    && pr.Description == description
                    && pr.Rating == rate
                    && pr.Title == title
                    && pr.UserName == userName))));
            _mockNotificationService.Verify(s => s.NotifyNewReviewsOnProduct(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void CollectRecentsFromProduct_NouveauxAvis()
        {
            var title = "title";
            var rate = "1.0 out of 5 stars";
            var userName = "user";
            var description = "description";
            var verifiedPurchase = "Verified Purchase";
            var idProduct = "AUDIFUEIDN";
            var dateAndPlace = "Reviewed in the United States on March 8, 2020";
            var idReview = "1";

            HtmlDocument docComplet = new HtmlDocument();
            docComplet.LoadHtml(GetDocumentString(title, rate, idReview, userName, description, verifiedPurchase, dateAndPlace));

            _mockHtmlService
                .SetupSequence(s => s.GetDocumentFromUrl(It.IsAny<string>()))
                .Returns(docComplet)
                .Returns(new HtmlDocument());

            _mockProductReviewService
                .Setup(s => s.SaveAllNew(It.IsAny<List<ProductReview>>()))
                .Returns(new List<ProductReview>
                {
                    new ProductReview { Id = idReview }
                });

            _service.CollectRecentsFromProduct(idProduct);

            _mockHtmlService.Verify(s => s.GetDocumentFromUrl("https://www.amazon.com/product-reviews/AUDIFUEIDN/?sortBy=recent&pageNumber=1"));
            _mockHtmlService.Verify(s => s.GetDocumentFromUrl("https://www.amazon.com/product-reviews/AUDIFUEIDN/?sortBy=recent&pageNumber=2"));
            _mockHtmlService.Verify(s => s.GetDocumentFromUrl(It.Is<string>(s => s.StartsWith("https://www.amazon.com/product-reviews/AUDIFUEIDN"))), Times.Exactly(2));

            _mockProductReviewService.Verify(s => s.SaveAllNew(It.Is<List<ProductReview>>(l =>
                l.Count == 1
                && l.Any(pr =>
                    pr.Id == idReview
                    && pr.IdProduct == idProduct
                    && pr.DateAndPlace == dateAndPlace
                    && pr.VerifiedPurchase == verifiedPurchase
                    && pr.Description == description
                    && pr.Rating == rate
                    && pr.Title == title
                    && pr.UserName == userName))));
            _mockNotificationService.Verify(s => s.NotifyNewReviewsOnProduct(idProduct));
        }

        private string GetDocumentString(string title, string rate, string idProductReview, string userName, string description, string verifiedPurchase, string dateAndPlace)
        {
            return @"<!DOCTYPE html>
                <html lang='en-us' class='a-no-js' data-19ax5a9jf='dingo'>
                    <head>
                        <meta charset='utf-8'/>
                        <meta http-equiv='x-dns-prefetch-control' content='on'>
                        <title>"+title+@"</title>
                    </head>
                    <body>
                        <div id='"+idProductReview+@"' data-hook='review'>
                            <div>
                                <div>
                                    <div>
                                        <a>
                                            <div >
                                                <span class='a-profile-name'>"+userName+@"</span>
                                            </div>
                                        </a>
                                    </div>
                                    <div>
                                        <a>
                                            <i data-hook='review-star-rating'>
                                                <span class='a-icon-alt'>"+rate+@"</span>
                                            </i>
                                        </a>
                                        <a data-hook='review-title'>
                                            <span>"+title+@"</span>
                                        </a>
                                    </div>
                                    <span data-hook='review-date'>"+dateAndPlace+@"</span>
                                    <div>
                                        <a>
                                            <span data-hook='avp-badge'>"+verifiedPurchase+@"</span>
                                        </a>
                                    </div>
                                    <div>
                                        <span data-hook='review-body'>
                                            <span>"+description+@"</span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </body>
                </html>";
        }
    }
}
