using System.ComponentModel.DataAnnotations;

namespace Core.ReviewsTracking.Entities
{
    /// <summary>
    /// Entité représentant le suivi d'un produit.
    /// </summary>
    public class ProductTracking
    {
        /// <summary>
        /// Identifiant ASIN du produit que l'on veut suivre.
        /// </summary>
        [Key]
        public string IdProduct { get; set; }
    }
}