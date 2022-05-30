using HtmlAgilityPack;

namespace Core.ReviewsCollection.Services
{
    /// <summary>
    /// Service de gestion 'éléments HTML.
    /// </summary>
    public interface IHtmlService
    {
        /// <summary>
        /// Récupère l'HTML à partir d'une URL et retourne un document HTML.
        /// </summary>
        /// <param name="url">L'url à partir de laquelle on veut récupérer l'HTML.</param>
        /// <returns>Le document HTML.</returns>
        HtmlDocument GetDocumentFromUrl(string url);
    }
}