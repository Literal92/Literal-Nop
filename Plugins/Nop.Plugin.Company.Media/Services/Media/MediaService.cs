using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;

namespace Nop.Plugin.Company.Media.Services
{
    public partial class MediaService : IMediaService
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
        private const string MediaModel_POINT_ALL_KEY = "Nop.MediaModel.all-{0}-{1}-{2}";
        private const string MediaModel_POINT_PATTERN_KEY = "Nop.MediaModel.";

        #endregion

        #region Fields


        private readonly ICacheManager _cacheManager;
        private readonly IRepository<Domain.Media> _MediaModelRepository;


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="SettingBehnabRepository">Store pickup point repository</param>
        public MediaService(ICacheManager cacheManager,
            IRepository<Domain.Media> storePickupPointRepository)
        {
            this._cacheManager = cacheManager;
            this._MediaModelRepository = storePickupPointRepository;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="MediaId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        public virtual IPagedList<Domain.Media> GetAllsMedia(int MediaId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var key = string.Format(MediaModel_POINT_ALL_KEY, pageIndex, pageSize, MediaId);
            return _cacheManager.Get(key, () =>
            {
                var query = _MediaModelRepository.Table;
                if (MediaId > 0)
                    query = query.Where(point => point.Id == MediaId || point.Id == 0);
                query = query.OrderBy(point => point.Id);

                return new PagedList<Domain.Media>(query, pageIndex, pageSize);
            });
        }

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        public virtual Domain.Media GetMediaById(int MediaId)
        {
            if (MediaId == 0)
                return null;

            return _MediaModelRepository.GetById(MediaId);
        }

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        public virtual void InsertMedia(Domain.Media MediaModel)
        {
            if (MediaModel == null)
                throw new ArgumentNullException(nameof(MediaModel));

            _MediaModelRepository.Insert(MediaModel);
            _cacheManager.RemoveByPattern(MediaModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        public virtual void UpdateMedia(Domain.Media MediaModel)
        {
            if (MediaModel == null)
                throw new ArgumentNullException(nameof(MediaModel));

            _MediaModelRepository.Update(MediaModel);
            _cacheManager.RemoveByPattern(MediaModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        public virtual void DeleteMedia(Domain.Media MediaModel)
        {
            if (MediaModel == null)
                throw new ArgumentNullException(nameof(MediaModel));

            _MediaModelRepository.Delete(MediaModel);
            _cacheManager.RemoveByPattern(MediaModel_POINT_PATTERN_KEY);
        }

        #endregion
    }
}
