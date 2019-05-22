using Nop.Core;
using Nop.Plugin.Faradata.AlarmShopping.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Faradata.AlarmShopping.Services
{
    public interface IFaraBotConfigService
    {
        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="Id">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        FaraBotConfig Get();
        
        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void Insert(FaraBotConfig MediaModel);

        /// <summary>
        /// Updates a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void Update(FaraBotConfig MediaModel);

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="MediaModel">Pickup point</param>
        void Delete(FaraBotConfig MediaModel);
    }
}
