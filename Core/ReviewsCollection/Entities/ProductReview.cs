namespace Core.ReviewsCollection.Entities
{
    /// <summary>
    /// Entité représentant l'avis laissé sur un produit.
    /// </summary>
    public class ProductReview
    {
        /// <summary>
        /// Identifiant de l'avis.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Note sous forme de texte.
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// Nom de l'utilisateur ayant laissé l'avis.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Titre de l'avis.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Contenu de l'avis.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Indique si l'avis concerne un achat vérifié.
        /// </summary>
        public string VerifiedPurchase { get; set; }

        /// <summary>
        /// Date et lieu de l'avis.
        /// </summary>
        public string DateAndPlace { get; set; }

        /// <summary>
        /// Numéri ASIN du produit concerné par l'avis.
        /// </summary>
        public string IdProduct { get; set; }
    }
}