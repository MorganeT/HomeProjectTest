namespace ReviewsTracking.UseCases.TrackProduct
{
    /// <summary>
    /// Use case de suivi de produits.
    /// </summary>
    public interface ITrackProductUseCase
    {
        /// <summary>
        /// Permet de suivre un nouveau produit.
        /// </summary>
        /// <param name="request">La requête concernant le nouveau produit à suivre.</param>
        void Track(TrackProductRequest request);
    }
}