using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Plugin.Faradata.Test.Domain;

namespace Nop.Plugin.Faradata.FirstTest.Services
{
    public class CustomerTokenService : ICustomerTokenService
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
        private readonly IRepository<CustomerToken> _TestRecordModelRepository;


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="SettingBehnabRepository">Store pickup point repository</param>
        public CustomerTokenService(ICacheManager cacheManager,
            IRepository<CustomerToken> storePickupPointRepository)
        {
            this._cacheManager = cacheManager;
            this._TestRecordModelRepository = storePickupPointRepository;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="TestId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        public virtual IPagedList<CustomerToken> GetAllsMedia(int TestId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var key = string.Format(MediaModel_POINT_ALL_KEY, pageIndex, pageSize, TestId);
            return _cacheManager.Get(key, () =>
            {
                var query = _TestRecordModelRepository.Table;
                if (TestId > 0)
                    query = query.Where(point => point.Id == TestId || point.Id == 0);
                query = query.OrderBy(point => point.Id);

                return new PagedList<CustomerToken>(query, pageIndex, pageSize);
            });
        }

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="Token">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        public virtual CustomerToken GetByToken(string Token)
        {
            if (Token == null || Token == "")
                return null;
            var query = _TestRecordModelRepository.Table;
            var rec = query.FirstOrDefault(a => a.Token.Equals(Token.Trim()));
            if (rec != null)
                return rec;
            return null;
        }

        public virtual string SetToken(int id, string deviceId)
        {
            var query = _TestRecordModelRepository.Table;
            var rec = query.FirstOrDefault(a => a.CustomerId == id);
            var token = Guid.NewGuid().ToString();
            if (rec == null)
            {
                _TestRecordModelRepository.Insert(new CustomerToken()
                {
                    CustomerId = id,
                    Token = token,
                    DeviceId = deviceId,
                    CreateDate = DateTime.UtcNow,
                    ExpireDate = DateTime.UtcNow.AddDays(1),
                });
            }
            else
            {
                rec.Token = token;
                _TestRecordModelRepository.Update(rec);
            }
            return token;
        }




        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        public virtual void Insert(CustomerToken MediaModel)
        {
            if (MediaModel == null)
                throw new ArgumentNullException(nameof(MediaModel));

            _TestRecordModelRepository.Insert(MediaModel);
            _cacheManager.RemoveByPattern(MediaModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Updates the pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        public virtual void UpdateMedia(CustomerToken MediaModel)
        {
            if (MediaModel == null)
                throw new ArgumentNullException(nameof(MediaModel));

            _TestRecordModelRepository.Update(MediaModel);
            _cacheManager.RemoveByPattern(MediaModel_POINT_PATTERN_KEY);
        }

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        public virtual void DeleteMedia(CustomerToken MediaModel)
        {
            if (MediaModel == null)
                throw new ArgumentNullException(nameof(MediaModel));

            _TestRecordModelRepository.Delete(MediaModel);
            _cacheManager.RemoveByPattern(MediaModel_POINT_PATTERN_KEY);
        }

        #endregion
    }
}
