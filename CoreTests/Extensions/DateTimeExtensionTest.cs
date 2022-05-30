using Core.ReviewsTracking.Extensions;
using NUnit.Framework;
using System;

namespace CoreTests.Extensions
{
    [TestFixture]
    internal class DateTimeExtensionTest
    {
        [Test]
        public void ToDateTimeFromDateAndPlaceString_Valide()
        {
            var dateAndPlace = "Reviewed in the United States on March 22, 2020";

            Assert.AreEqual(new DateTime(2020,3,22), dateAndPlace.ToDateTimeFromDateAndPlaceString());
        }

        [Test]
        public void ToDateTimeFromDateAndPlaceString_ParsingImpossible()
        {
            var dateAndPlace = "Other text with no date";

            Assert.Null(dateAndPlace.ToDateTimeFromDateAndPlaceString());
        }
    }
}
