using BasketAPI.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Data.Interfaces
{
    /// <summary>
    /// Data context interface
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Baskets collection
        /// </summary>
        ICollection<Basket> Baskets { get; set; }
    }
}
