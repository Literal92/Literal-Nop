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
    public class FaraBotUserService : IFaraBotUserService
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
        private readonly IRepository<FaraBotUser> _ModelRepository;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="SettingBehnabRepository">Store pickup point repository</param>
        public FaraBotUserService(ICacheManager cacheManager,
            IRepository<FaraBotUser> storePickupPointRepository)
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
        public virtual IPagedList<FaraBotUser> Get(int Id = 0, int pageIndex = 0, int pageSize = 10)
        {
            var key = string.Format(model_POINT_ALL_KEY, pageIndex, pageSize, Id);
            return _cacheManager.Get(key, () =>
            {
                var query = _ModelRepository.Table;
                if (Id > 0)
                    query = query.Where(a => a.Id == Id || a.Id == 0);
                query = query.OrderBy(point => point.Id);

                return new PagedList<FaraBotUser>(query, pageIndex, pageSize);
            });
        }
        public virtual IList<FaraBotUser> Get(byte step)
        {
            var query = _ModelRepository.Table;
            query = query.Where(a => a.Step == step).OrderBy(a => a.Id);
            return query.ToList();
        }

        /// <summary>
        /// Get Customer by ChatId
        /// </summary>
        /// <param name="ChatId">ChatId User</param>
        /// <returns>Pickup point</returns>
        public virtual FaraBotUser GetByChatId(long ChatId)
        {
            var d = ChatId;
            if (ChatId == null || ChatId == 0)
                return null;
            var query = _ModelRepository.Table;
            var rec = query.Where(a => a.ChatId == ChatId).FirstOrDefault();
            if (rec != null)
                return rec;
            return null;
        }

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="model">Pickup point</param>
        public virtual void Insert(FaraBotUser model)
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
        public virtual void Update(FaraBotUser model)
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
        public virtual void Delete(FaraBotUser model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _ModelRepository.Delete(model);
            _cacheManager.RemoveByPattern(model_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="model">Pickup point</param>
        public virtual void DeleteUser(int id)
        {
            if (id!=0)
            {
                var model = _ModelRepository.GetById(id);
                _ModelRepository.Delete(model);
                _cacheManager.RemoveByPattern(model_POINT_PATTERN_KEY);
            }
        }

        #endregion
    }
}
