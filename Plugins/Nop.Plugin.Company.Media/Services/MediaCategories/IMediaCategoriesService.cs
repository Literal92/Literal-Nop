using Nop.Core;

namespace Nop.Plugin.Company.Media.Services.MediaCategories
{    
    public partial interface IMediaCategoriesService
    {
        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="MediaCategoriesId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        IPagedList<Domain.MediaCategories> GetAllsMediaCategories(int MediaCategoriesId = 0, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaCategoriesId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        Domain.MediaCategories GetMediaCategoriesById(int MediaCategoriesId);

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaCategoriesModel">Pickup point</param>
        void InsertMediaCategories(Domain.MediaCategories MediaCategoriesModel);

        /// <summary>
        /// Updates a pickup point
        /// </summary>
        /// <param name="MediaCategoriesModel">Pickup point</param>
        void UpdateMediaCategories(Domain.MediaCategories MediaCategoriesModel);

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaCategoriesModel">Pickup point</param>
        void DeleteMediaCategories(Domain.MediaCategories MediaCategoriesModel);


        Domain.MediaCategory GetLastCategory(int MediaId);

        void DeleteCategoriesByMediaId(int MediaId);

    }
}
