using Nop.Core;
using Nop.Plugin.Faradata.AlarmShopping.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.AlarmShopping.Services
{
    public interface IFaraBotUserService
    {
        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="Id">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        //IPagedList<FaraBotUser> Get(int Id = 0, int pageIndex = 0, int pageSize = 10);
        IList<FaraBotUser> Get(byte step);

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="MediaId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        FaraBotUser GetByChatId(long ChatId);

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void Insert(FaraBotUser MediaModel);

        /// <summary>
        /// Updates a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void Update(FaraBotUser MediaModel);

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void Delete(FaraBotUser MediaModel);

        /// <summary>
        /// Deletes user
        /// </summary>
        void DeleteUser(int id);
    }
}
