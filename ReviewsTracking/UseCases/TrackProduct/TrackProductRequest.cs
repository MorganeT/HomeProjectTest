namespace ReviewsTracking.UseCases.TrackProduct
{
    /// <summary>
    /// Requête de suivi d'un nouveau produit.
    /// </summary>
    public class TrackProductRequest
    {
        /// <summary>
        /// Identifiant ASIN du produit à suivre.
        /// </summary>
        public string IdProduct { get; set; }
    }
}