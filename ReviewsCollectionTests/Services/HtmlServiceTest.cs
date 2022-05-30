using NUnit.Framework;
using ReviewsCollection.Services;

namespace ReviewsCollectionTests.Services
{

    [TestFixture]
    internal class HtmlServiceTest
    {
        private HtmlService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new HtmlService();
        }

        [Test]
        public void GetDocumentFromUrlValide()
        {
            var doc = _service.GetDocumentFromUrl("http://google.fr");
            Assert.NotNull(doc);
        }
    }
}
