using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Models.Data
{
    /// <summary>
    /// Basket Data Model
    /// </summary>
    public class Basket
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public Basket()
        {
            CreationDate = DateTime.UtcNow;
            LastUpdateDate = DateTime.UtcNow;
            BasketItems = new HashSet<BasketItem>();
        }

        /// <summary>
        /// The Basket Cart Id = User Id
        /// </summary>
        public string CartId { get; set; }

        /// <summary>
        /// Creation Date
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Last Update Date
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// Items of The Basket
        /// </summary>
        public ICollection<BasketItem> BasketItems { get; set; }

        /// <summary>
        /// Total Price of Basket Items
        /// </summary>
        public decimal TotalPrice
        {
            get
            {
                return (BasketItems != null && BasketItems.Count > 0) ? BasketItems.Select(i => i.Price * i.Quantity).Sum() : 0;
            }
        }
    }
}
