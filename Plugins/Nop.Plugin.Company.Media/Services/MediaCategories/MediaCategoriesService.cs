using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;

namespace Nop.Plugin.Company.Media.Services.MediaCategories
{    
    public partial class MediaCategoriesService : IMediaCategoriesService
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
        private const string MediaCategoriesModel_POINT_ALL_KEY = "Nop.MediaCategoriesModel.all-{0}-{1}-{2}";
        private const string MediaCategoriesModel_POINT_PATTERN_KEY = "Nop.MediaCategoriesModel.";

        #endregion

        #region Fields


        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Domain.MediaCategories> _MediaCategoriesModelRepository;
        private readonly IRepository<Media.Domain.Media> _MediaModelRepository;
        private readonly IRepository<Domain.MediaCategories> _MediaCategoriesRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="SettingBehnabRepository">Store pickup point repository</param>
        public MediaCategoriesService(ICacheManager cacheManager,
            IRepository<Domain.MediaCategories> storePickupPointRepository,
            IRepository<Media.Domain.Media> MediaModelRepository,
            IRepository<Domain.MediaCategories> MediaCategoriesRepository
            )
        {
            this._cacheManager = cacheManager;
            this._MediaCategoriesModelRepository = storePickupPointRepository;
            this._MediaModelRepository = MediaModelRepository;
            this._MediaCategoriesRepository = MediaCategoriesRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="MediaCategoriesId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        public virtual IPagedList<Domain.MediaCategories> GetAllsMediaCategories(int MediaCategoriesId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var key = string.Format(MediaCategoriesModel_POINT_ALL_KEY, pageIndex, pageSize, MediaCategoriesId);
            return _cacheManager.Get(key, () =>
            {
                var query = _MediaCategoriesModelRepository.Table;

                if (MediaCategoriesId > 0)
                    query = query.Where(point => point.Id == MediaCategoriesId || point.Id == 0);
                query = query.OrderBy(point => point.Id);

                return new PagedList<Domain.MediaCategories>(query, pageIndex, pageSize);
            });
        }


        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaCategoriesId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        public virtual Domain.MediaCategories GetMediaCategoriesById(int MediaCategoriesId)
        {
            if (MediaCategoriesId == 0)
                return null;

            return _MediaCategoriesModelRepository.GetById(MediaCategoriesId);
        }

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaCategoriesModel">Pickup point</param>
        public virtual void InsertMediaCategories(Domain.MediaCategories MediaCategoriesModel)
        {
            if (MediaCategoriesModel == null)
                throw new ArgumentNullException(nameof(MediaCategoriesModel));
            _MediaCategoriesModelRepository.Insert(MediaCategoriesModel);
            _cacheManager.RemoveByPattern(MediaCategoriesModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the pickup point
        /// </summary>
        /// <param name="MediaCategoriesModel">Pickup point</param>
        public virtual void UpdateMediaCategories(Domain.MediaCategories MediaCategoriesModel)
        {
            if (MediaCategoriesModel == null)
                throw new ArgumentNullException(nameof(MediaCategoriesModel));

            _MediaCategoriesModelRepository.Update(MediaCategoriesModel);
            _cacheManager.RemoveByPattern(MediaCategoriesModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaCategoriesModel">Pickup point</param>
        public virtual void DeleteMediaCategories(Domain.MediaCategories MediaCategoriesModel)
        {
            if (MediaCategoriesModel == null)
                throw new ArgumentNullException(nameof(MediaCategoriesModel));

            _MediaCategoriesModelRepository.Delete(MediaCategoriesModel);
            _cacheManager.RemoveByPattern(MediaCategoriesModel_POINT_PATTERN_KEY);
        }

        public virtual Domain.MediaCategory GetLastCategory(int MediaId)
        {
            var query = _MediaCategoriesModelRepository.Table;
            query = query.Where(a => a.MediaId == MediaId);
            query = query.OrderByDescending(a => a.MediaCategoryId);
            return query.FirstOrDefault().MediaCategory;
        }

        public virtual void DeleteCategoriesByMediaId(int MediaId)
        {
            var query = _MediaCategoriesModelRepository.Table;
            query = query.Where(point => point.MediaId == MediaId);
            var list = query.ToList();
            foreach (var item in list)
            {
                DeleteMediaCategories(item);
            }
        }

        #endregion
    }
}
