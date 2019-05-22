using Nop.Core;
using Nop.Plugin.Faradata.Test.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.FirstTest.Services
{
    public interface ICustomerTokenService
    {
        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="MediaId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        IPagedList<CustomerToken> GetAllsMedia(int MediaId = 0, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        CustomerToken GetByToken(string Token);

        string SetToken(int id,string deviceId);

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void Insert(CustomerToken MediaModel);

        /// <summary>
        /// Updates a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void UpdateMedia(CustomerToken MediaModel);

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void DeleteMedia(CustomerToken MediaModel);
    }
}
