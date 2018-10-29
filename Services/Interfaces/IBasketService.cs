using BasketAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Services.Interfaces
{
    /// <summary>
    /// Basket Service Interface
    /// </summary>
    public interface IBasketService
    {
        /// <summary>
        /// Gets a Basket DTO
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <returns>Return the basket object if found</returns>
        Task<Basket> GetBasketById(string basketId);
        /// <summary>
        /// Get a Basket Item by id
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <returns>Return the basket item object</returns>
        Task<BasketItem> GetBasketItemById(string basketId, string itemId);
        /// <summary>
        /// Add a new item to basket
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="item">Basket Item DTO</param>
        /// <returns>Return the basket object after adding the item</returns>
        Task<Basket> AddItemToBasket(string basketId, BasketItem item);
        /// <summary>
        /// Set an item quantity to a given value
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <param name="quantity">Item Quantity To Be Set</param>
        /// <returns>Return the basket object after setting the item quantity</returns>
        Task<Basket> SetItemQuantity(string basketId, string itemId, int quantity);
        /// <summary>
        /// Delete a basket and all its items
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <returns>Return ture if the basket was delete successfully</returns>
        Task<bool> DeleteBasket(string basketId);
        /// <summary>
        /// Delete an item from basket
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <returns>Return the basket object after deleting the item</returns>
        Task<Basket> DeleteItemFromBasket(string basketId, string itemId);
        /// <summary>
        /// Check if a basket exists or not
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <returns>True if the basket exists and false if not</returns>
        Task<bool> IsBasketExist(string basketId);
        /// <summary>
        /// Check if a basket item exists or not
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <returns>True if item exists in a basket and false if not</returns>
        Task<bool> IsItemExist(string basketId, string itemId);
    }
}
