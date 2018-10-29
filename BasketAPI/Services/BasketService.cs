using AutoMapper;
using BasketAPI.Data.Interfaces;
using BasketAPI.Models.DTO;
using BasketAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketAPI.Services
{
    /// <summary>
    /// Basket Service
    /// </summary>
    public class BasketService : IBasketService
    {
        #region Properties
        /// <summary>
        /// Data Context
        /// </summary>
        private readonly IDataContext _dataContext;

        /// <summary>
        /// Automapper
        /// </summary>
        private readonly IMapper _mapper;
        #endregion

        #region CTOR
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="dataContext">Data Context</param>
        public BasketService(IDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        #endregion

        #region Services
        /// <summary>
        /// Gets a Basket DTO
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <returns>Return the basket object if found</returns>
        public async Task<Basket> GetBasketById(string basketId)
        {
            var basket = _dataContext.Baskets.FirstOrDefault(i => i.CartId == basketId);
            return _mapper.Map<Basket>(basket ?? new Models.Data.Basket());
        }

        /// <summary>
        /// Get a Basket Item by id
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <returns>Return the basket item object</returns>
        public async Task<BasketItem> GetBasketItemById(string basketId, string itemId)
        {
            var basket = _dataContext.Baskets.FirstOrDefault(i => i.CartId == basketId);
            var basketItem = basket?.BasketItems?.FirstOrDefault(i => i.ItemId == itemId);
            return _mapper.Map<BasketItem>(basketItem);
        }

        /// <summary>
        /// Add a new item to basket
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="item">Basket Item DTO</param>
        /// <returns>Return the basket object after adding the item</returns>
        public async Task<Basket> AddItemToBasket(string basketId, BasketItem item)
        {
            var basket = _dataContext.Baskets.FirstOrDefault(i => i.CartId == basketId);
            // check if the basket exists
            if (basket != null)
            {
                // If the item is already in the list and added again, then we add the quantity of the added item to the existing item
                if (basket.BasketItems.Any(i => i.ItemId == item.ItemId))
                {
                    var basketItem = basket.BasketItems.FirstOrDefault();
                    basket.LastUpdateDate = DateTime.UtcNow;
                    basketItem.Quantity += item.Quantity;
                }
                // Else create the basket item object and insert it in the database
                else
                {
                    var basketItem = new BasketAPI.Models.Data.BasketItem
                    {
                        ItemId = item.ItemId,
                        ItemName = item.ItemName,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    basket.BasketItems.Add(basketItem);
                    basket.LastUpdateDate = DateTime.UtcNow;
                }
            }
            // if basket doesn't exit we create a new one
            else
            {
                basket = new Models.Data.Basket
                {
                    CartId = basketId
                };
                var basketItem = new BasketAPI.Models.Data.BasketItem
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                basket.BasketItems.Add(basketItem);
                _dataContext.Baskets.Add(basket);
            }
            return _mapper.Map<Basket>(basket);
        }

        /// <summary>
        /// Set an item quantity to a given value
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <param name="quantity">Item Quantity To Be Set</param>
        /// <returns>Return the basket object after setting the item quantity</returns>
        public async Task<Basket> SetItemQuantity(string basketId, string itemId, int quantity)
        {
            var basket = _dataContext.Baskets.FirstOrDefault(i => i.CartId == basketId);
            // check if the basket exists
            if (basket != null)
            {
                // If the item is already in the list change the quantity of it
                if (basket.BasketItems.Any(i => i.ItemId == itemId))
                {
                    var basketItem = basket.BasketItems.FirstOrDefault();
                    basket.LastUpdateDate = DateTime.UtcNow;
                    basketItem.Quantity = quantity;
                }
            }
            return _mapper.Map<Basket>(basket);
        }

        /// <summary>
        /// Delete a basket and all its items
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <returns>Return ture if the basket was delete successfully</returns>
        public async Task<bool> DeleteBasket(string basketId)
        {
            var toRemove = _dataContext.Baskets.FirstOrDefault(i => i.CartId == basketId);
            if (toRemove != null)
                _dataContext.Baskets.Remove(toRemove);
            else
                return false;
            return true;
        }

        /// <summary>
        /// Delete an item from basket
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <returns>Return the basket object after deleting the item</returns>
        public async Task<Basket> DeleteItemFromBasket(string basketId, string itemId)
        {
            var basket = _dataContext.Baskets.FirstOrDefault(i => i.CartId == basketId);
            if (basket != null)
            {
                var toRemove = basket?.BasketItems?.FirstOrDefault(i => i.ItemId == itemId);
                if (toRemove != null)
                {
                    basket.BasketItems?.Remove(toRemove);
                    basket.LastUpdateDate = DateTime.UtcNow;
                }
            }
            return _mapper.Map<Basket>(basket);
        }

        /// <summary>
        /// Check if a basket exists or not
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <returns>True if the basket exists and false if not</returns>
        public async Task<bool> IsBasketExist(string basketId)
        {
            return _dataContext.Baskets.Any(i => i.CartId == basketId);
        }

        /// <summary>
        /// Check if a basket item exists or not
        /// </summary>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Basket Item Id</param>
        /// <returns>True if item exists in a basket and false if not</returns>
        public async Task<bool> IsItemExist(string basketId, string itemId)
        {
            return _dataContext.Baskets.Any(i => i.CartId == basketId) && _dataContext.Baskets.FirstOrDefault(i => i.CartId == basketId).BasketItems.Any(i => i.ItemId == itemId);
        }
        #endregion
    }
}
