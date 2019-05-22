using Nop.Core;

namespace Nop.Plugin.Company.Media.Services.MediaCategory
{    
    public partial interface IMediaCategoryService
    {
        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="MediaCategoryId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        IPagedList<Domain.MediaCategory> GetAllsMediaCategory(int MediaCategoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue);
        IPagedList<Domain.MediaCategory> GetMediaCategoryByLevelAndParentId(int ParentMediaCategoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue);
        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaCategoryId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        Domain.MediaCategory GetMediaCategoryById(int MediaCategoryId);

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaCategoryModel">Pickup point</param>
        void InsertMediaCategory(Domain.MediaCategory MediaCategoryModel);

        /// <summary>
        /// Updates a pickup point
        /// </summary>
        /// <param name="MediaCategoryModel">Pickup point</param>
        void UpdateMediaCategory(Domain.MediaCategory MediaCategoryModel);

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaCategoryModel">Pickup point</param>
        void DeleteMediaCategory(Domain.MediaCategory MediaCategoryModel);

        string CategoryNavigation(int CategoryId);

    }
}
