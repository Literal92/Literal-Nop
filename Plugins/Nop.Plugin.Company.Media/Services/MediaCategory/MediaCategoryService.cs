using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;

namespace Nop.Plugin.Company.Media.Services.MediaCategory
{
    
    public partial class MediaCategoryService : IMediaCategoryService
    {
        #region Constants

        /// <summary>
        /// Cache key for pickup points
        /// </summary>
        /// <remarks>
        /// {0} : page index
        /// {1} : page size
        /// {2} : current store ID
        /// </remarks>
        private const string MediaCategoryModel_POINT_ALL_KEY = "Nop.MediaCategoryModel.all-{0}-{1}-{2}";
        private const string MediaCategoryModel_POINT_PATTERN_KEY = "Nop.MediaCategoryModel.";

        #endregion

        #region Fields


        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Domain.MediaCategory> _MediaCategoryModelRepository;
        private readonly IRepository<Media.Domain.Media> _MediaModelRepository;
        private readonly IRepository<Domain.MediaCategory> _MediaCategoryRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="SettingBehnabRepository">Store pickup point repository</param>
        public MediaCategoryService(ICacheManager cacheManager,
            IRepository<Domain.MediaCategory> storePickupPointRepository,
            IRepository<Media.Domain.Media> MediaModelRepository,
            IRepository<Domain.MediaCategory> MediaCategoryRepository
            )
        {
            this._cacheManager = cacheManager;
            this._MediaCategoryModelRepository = storePickupPointRepository;
            this._MediaModelRepository = MediaModelRepository;
            this._MediaCategoryRepository = MediaCategoryRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="MediaCategoryId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        public virtual IPagedList<Domain.MediaCategory> GetAllsMediaCategory(int MediaCategoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var key = string.Format(MediaCategoryModel_POINT_ALL_KEY, pageIndex, pageSize, MediaCategoryId);
            return _cacheManager.Get(key, () =>
            {
                var query = _MediaCategoryModelRepository.Table;
                query = query.Where(a => a.MediaCategoryStatus != Domain.MediaCategory.StatusType.Deleted);
                if (MediaCategoryId > 0)
                    query = query.Where(point => point.Id == MediaCategoryId || point.Id == 0);
                query = query.OrderBy(point => point.Id);

                return new PagedList<Domain.MediaCategory>(query, pageIndex, pageSize);
            });
        }
        public virtual IPagedList<Domain.MediaCategory> GetMediaCategoryByLevelAndParentId(int ParentMediaCategoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var key = string.Format(MediaCategoryModel_POINT_ALL_KEY, pageIndex, pageSize, ParentMediaCategoryId);
            return _cacheManager.Get(key, () =>
            {
                var query = _MediaCategoryModelRepository.Table;
                query = query.Where(a => a.MediaCategoryStatus != Domain.MediaCategory.StatusType.Deleted);
                query = query.Where(a => a.MediaCategoryParentId == ParentMediaCategoryId);
                query = query.OrderBy(point => point.Id);

                return new PagedList<Domain.MediaCategory>(query, pageIndex, pageSize);
            });

        }

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaCategoryId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        public virtual Domain.MediaCategory GetMediaCategoryById(int MediaCategoryId)
        {
            if (MediaCategoryId == 0)
                return null;

            return _MediaCategoryModelRepository.GetById(MediaCategoryId);
        }

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaCategoryModel">Pickup point</param>
        public virtual void InsertMediaCategory(Domain.MediaCategory MediaCategoryModel)
        {
            if (MediaCategoryModel == null)
                throw new ArgumentNullException(nameof(MediaCategoryModel));
            _MediaCategoryModelRepository.Insert(MediaCategoryModel);
            _cacheManager.RemoveByPattern(MediaCategoryModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the pickup point
        /// </summary>
        /// <param name="MediaCategoryModel">Pickup point</param>
        public virtual void UpdateMediaCategory(Domain.MediaCategory MediaCategoryModel)
        {
            if (MediaCategoryModel == null)
                throw new ArgumentNullException(nameof(MediaCategoryModel));

            _MediaCategoryModelRepository.Update(MediaCategoryModel);
            _cacheManager.RemoveByPattern(MediaCategoryModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaCategoryModel">Pickup point</param>
        public virtual void DeleteMediaCategory(Domain.MediaCategory MediaCategoryModel)
        {
            if (MediaCategoryModel == null)
                throw new ArgumentNullException(nameof(MediaCategoryModel));

            _MediaCategoryModelRepository.Delete(MediaCategoryModel);
            _cacheManager.RemoveByPattern(MediaCategoryModel_POINT_PATTERN_KEY);
        }

        public virtual string CategoryNavigation(int CategoryId)
        {


            var Parent = true;
            var Navigation = "";
            int count = 0;
            while (Parent)
            {
                var query = _MediaCategoryModelRepository.Table;
                query = query.Where(a => a.MediaCategoryStatus != Domain.MediaCategory.StatusType.Deleted);
                query = query.Where(a => a.Id == CategoryId);
                var category = query.FirstOrDefault();
                if (category != null)
                {
                    if (count == 0)
                    {
                        Navigation = category.MediaCategoryName;
                        CategoryId = category.MediaCategoryParentId;
                        count = 1;
                    }
                    else
                    {
                        Navigation = category.MediaCategoryName + " --- " + " > " + Navigation;
                        CategoryId = category.MediaCategoryParentId;
                    }

                }
                else
                {
                    Parent = false;
                }
            }
            return Navigation;

        }

        #endregion
    }
}
