using System;
using System.Text.RegularExpressions;

namespace Core.ReviewsTracking.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// Convertit le lieu est la date du commentaire saisi sous forme de chaîne de caractères en <see cref="DateTime"/>
        /// </summary>
        public static DateTime? ToDateTimeFromDateAndPlaceString(this string dateAndPlace)
        {
            DateTime result;

            var pattern = "Reviewed.*?on ([a-zA-Z]+ [0-9]{1,2}, [0-9]{4})";
            var newDate = Regex.Replace(dateAndPlace, pattern, "$1", RegexOptions.IgnoreCase);

            var successParse = DateTime.TryParse(newDate, out result);

            return successParse ? result : default(DateTime?);
        }
    }
}
