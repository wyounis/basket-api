using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BasketAPI.Models.DTO;
using BasketAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace BasketAPI.Controllers
{
    /// <summary>
    /// Baskets controller
    /// </summary>
    [Route("beta/baskets")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        #region Properties
        /// <summary>
        /// Basket Service
        /// </summary>
        private readonly IBasketService _basketService;
        #endregion

        #region CTOR
        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="basketService">Basket Service</param>
        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        #endregion

        #region APIs
        /// <summary>
        /// beta/baskets/1234
        /// </summary>
        /// <remarks>Returns a shopping basket by its id
        /// Prerequisites: 
        /// - Having a valid basket id/user id</remarks>
        /// <param name="basketId">Shopping Basket Id</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid Input</response>
        [HttpGet("{basketId}")]
        [SwaggerOperation(OperationId = "GetBasketById")]
        [SwaggerResponse(200, "Successful operation", typeof(Basket))]
        [SwaggerResponse(400, "Invalid Input")]
        public async Task<IActionResult> GetById([FromRoute]string basketId)
        {
            var basket = await _basketService.GetBasketById(basketId);
            return Ok(await _basketService.GetBasketById(basketId));
        }

        /// <summary>
        /// beta/baskets/1234
        /// </summary>
        /// <remarks>Add a new item to shopping basket or create one if the basket doesn't exist
        /// Prerequisites: 
        /// - Having a valid basket id/user id</remarks>
        /// <param name="basketId">Shopping Basket Id to add the item to</param>
        /// <param name="item">Item object details to be added</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid Input</response>
        [HttpPost("{basketId}")]
        [SwaggerOperation(OperationId = "PostItem")]
        [SwaggerResponse(200, "Successful operation", typeof(Basket))]
        [SwaggerResponse(400, "Invalid Input")]
        public async Task<IActionResult> Post([FromRoute]string basketId, [FromBody]BasketItem item)
        {
            return Ok(await _basketService.AddItemToBasket(basketId, item));
        }

        /// <summary>
        /// beta/baskets/1234
        /// </summary>
        /// <remarks>Clear the whole shopping basket
        /// Prerequisites: 
        /// - Having a valid basket id/user id</remarks>
        /// <param name="basketId">Shopping Basket Id to be cleared</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid Input</response>
        [HttpDelete("{basketId}")]
        [SwaggerOperation(OperationId = "DeleteBasket")]
        [SwaggerResponse(200, "Successful operation", typeof(bool))]
        [SwaggerResponse(400, "Invalid Input")]
        public async Task<IActionResult> DeleteBasket([FromRoute]string basketId)
        {
            if (!await _basketService.IsBasketExist(basketId))
            {
                ModelState.AddModelError("basketId", "Basket doesn't exist");
                return BadRequest(ModelState);
            }
            return Ok(await _basketService.DeleteBasket(basketId));
        }

        /// <summary>
        /// beta/baskets/1234/items/5678/quantity/10
        /// </summary>
        /// <remarks>Update quantity of item in shopping basket
        /// Prerequisites: 
        /// - Having a valid basket id/user id
        /// - Having a valid item id within the given basket</remarks>
        /// <param name="basketId">Shopping Basket Id for the item to be updated</param>
        /// <param name="itemId">Item Id to be updated</param>
        /// <param name="quantity">Quantity of item to be set (must be greater than 0)</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid Input</response>
        [HttpPatch("{basketId}/items/{itemId}/quantity/{quantity}")]
        [SwaggerOperation(OperationId = "PatchItemQuantity")]
        [SwaggerResponse(200, "Successful operation", typeof(Basket))]
        [SwaggerResponse(400, "Invalid Input")]
        public async Task<IActionResult> PatchQuantity([FromRoute]string basketId, [FromRoute]string itemId, [FromRoute][Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]int quantity)
        {
            if (!await _basketService.IsBasketExist(basketId))
            {
                ModelState.AddModelError("basketId", "Basket doesn't exist");
                return BadRequest(ModelState);
            }
            else if (!await _basketService.IsItemExist(basketId, itemId))
            {
                ModelState.AddModelError("itemId", "Item doesn't exist");
                return BadRequest(ModelState);
            }
            return Ok(await _basketService.SetItemQuantity(basketId, itemId, quantity));
        }

        /// <summary>
        /// beta/baskets/1234/items/5678
        /// </summary>
        /// <remarks>Delete item from shopping basket
        /// Prerequisites: 
        /// - Having a valid basket id/user id
        /// - Having a valid item id within the given basket</remarks>
        /// <param name="basketId">Shopping Basket Id for the item to be deleted</param>
        /// <param name="itemId">Item Id to be deleted</param>
        /// <response code="200">Successful operation</response>
        /// <response code="400">Invalid Input</response>
        [HttpDelete("{basketId}/items/{itemId}")]
        [SwaggerOperation(OperationId = "DeleteBasketItem")]
        [SwaggerResponse(200, "Successful operation", typeof(Basket))]
        [SwaggerResponse(400, "Invalid Input")]
        public async Task<IActionResult> DeleteBasketItem([FromRoute]string basketId, [FromRoute]string itemId)
        {
            if (!await _basketService.IsBasketExist(basketId))
            {
                ModelState.AddModelError("basketId", "Basket doesn't exist");
                return BadRequest(ModelState);
            }
            else if (!await _basketService.IsItemExist(basketId, itemId))
            {
                ModelState.AddModelError("itemId", "Item doesn't exist");
                return BadRequest(ModelState);
            }
            return Ok(await _basketService.DeleteItemFromBasket(basketId, itemId));
        }
        #endregion
    }
}
