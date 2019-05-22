using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Plugin.Faradata.AlarmShopping.Domain;

namespace Nop.Plugin.Faradata.AlarmShopping.Services
{
    public class FaraBotConfigService : IFaraBotConfigService
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
        private const string model_POINT_ALL_KEY = "Nop.model.all-{0}-{1}-{2}";
        private const string model_POINT_PATTERN_KEY = "Nop.model.";

        #endregion

        #region Fields
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<FaraBotConfig> _ModelRepository;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="SettingBehnabRepository">Store pickup point repository</param>
        public FaraBotConfigService(ICacheManager cacheManager,
            IRepository<FaraBotConfig> storePickupPointRepository)
        {
            this._cacheManager = cacheManager;
            this._ModelRepository = storePickupPointRepository;

        }
        #endregion

        #region Methods

        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="Id">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        public virtual FaraBotConfig Get()
        {
            var query = _ModelRepository.Table;
            var cng = query.FirstOrDefault();
            return cng;
        }

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="model">Pickup point</param>
        public virtual void Insert(FaraBotConfig model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _ModelRepository.Insert(model);
            _cacheManager.RemoveByPattern(model_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the pickup point
        /// </summary>
        /// <param name="model">Pickup point</param>
        public virtual void Update(FaraBotConfig model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _ModelRepository.Update(model);
            _cacheManager.RemoveByPattern(model_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="model">Pickup point</param>
        public virtual void Delete(FaraBotConfig model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _ModelRepository.Delete(model);
            _cacheManager.RemoveByPattern(model_POINT_PATTERN_KEY);
        }

        #endregion
    }
}
