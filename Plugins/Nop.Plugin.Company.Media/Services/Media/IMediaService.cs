using Nop.Core;

namespace Nop.Plugin.Company.Media.Services
{
    public partial interface IMediaService
    {
        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="MediaId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        IPagedList<Domain.Media> GetAllsMedia(int MediaId = 0, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        Domain.Media GetMediaById(int MediaId);

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void InsertMedia(Domain.Media MediaModel);

        /// <summary>
        /// Updates a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void UpdateMedia(Domain.Media MediaModel);

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void DeleteMedia(Domain.Media MediaModel);
    }
}
