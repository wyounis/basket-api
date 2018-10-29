using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Models.DTO
{
    /// <summary>
    /// Basket Item Data Transfer Object
    /// </summary>
    public class BasketItem
    {
        /// <summary>
        /// Item Id
        /// </summary>
        [Required(ErrorMessage = "ItemId is required")]
        public string ItemId { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        [Required(ErrorMessage = "ItemName is required")]
        public string ItemName { get; set; }

        /// <summary>
        /// Item Price
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.0")]
        public decimal Price { get; set; }

        /// <summary>
        /// Item Quantity in Basket
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
