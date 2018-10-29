using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Models.Data
{
    /// <summary>
    /// Basket Item Data Model
    /// </summary>
    public class BasketItem
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public BasketItem()
        {
            Price = 0;
            Quantity = 0;
            AddedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Item Id
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Item Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Item Quantity in Basket
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Time it was added to cart
        /// </summary>
        public DateTime AddedAt { get; set; }
    }
}
