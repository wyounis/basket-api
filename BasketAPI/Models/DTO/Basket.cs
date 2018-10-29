using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Models.DTO
{
    /// <summary>
    /// Basket Data Transfer Object
    /// </summary>
    public class Basket
    {
        /// <summary>
        /// Items of The Basket
        /// </summary>
        public List<BasketItem> BasketItems { get; set; }

        /// <summary>
        /// Total Price of Basket Items
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
