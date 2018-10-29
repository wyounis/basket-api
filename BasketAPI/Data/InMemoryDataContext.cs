using BasketAPI.Data.Interfaces;
using BasketAPI.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Data
{
    /// <summary>
    /// In Memory Data Context
    /// </summary>
    public class InMemoryDataContext : IDataContext
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public InMemoryDataContext()
        {
            Baskets = new HashSet<Basket>();
        }

        /// <summary>
        /// Baskets Collection
        /// </summary>
        public ICollection<Basket> Baskets { get; set; }
    }
}
