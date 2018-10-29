using AutoMapper;
using BasketAPI.Controllers;
using BasketAPI.Data;
using BasketAPI.Models.Data;
using BasketAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BasketAPI.Testing.Controllers
{
    public class BasketsControllerTest
    {
        [Fact]
        public async Task GetById_ReturnsOkObjectResult()
        {
            // Arrange
            var mockMapper = MockIMapper();
            var inmemoryDataContext = new InMemoryDataContext();
            inmemoryDataContext.Baskets = GetTestBasket();
            var basketService = new BasketService(inmemoryDataContext, mockMapper.Object);
            var basketsController = new BasketsController(basketService);

            // Act
            var result = await basketsController.GetById("123");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBasketItem_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var mockMapper = MockIMapper();
            var inmemoryDataContext = new InMemoryDataContext();
            inmemoryDataContext.Baskets = GetTestBasket();
            var basketService = new BasketService(inmemoryDataContext, mockMapper.Object);
            var basketsController = new BasketsController(basketService);

            // Act
            var result = await basketsController.DeleteBasketItem("000", "00");

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        private IMock<IMapper> MockIMapper()
        {
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<BasketAPI.Models.DTO.Basket>(It.IsAny<BasketAPI.Models.Data.Basket>()))
                .Returns((BasketAPI.Models.Data.Basket source) =>
                {
                    return new BasketAPI.Models.DTO.Basket
                    {
                        TotalPrice = source.TotalPrice,
                        BasketItems = source.BasketItems.Select(i => new Models.DTO.BasketItem { ItemId = i.ItemId, ItemName = i.ItemName, Price = i.Price, Quantity = i.Quantity }).ToList()
                    };
                });
            mockMapper.Setup(x => x.Map<BasketAPI.Models.DTO.BasketItem>(It.IsAny<BasketAPI.Models.Data.BasketItem>()))
                .Returns((BasketAPI.Models.Data.BasketItem source) =>
                {
                    return new BasketAPI.Models.DTO.BasketItem
                    {
                        ItemId = source.ItemId,
                        ItemName = source.ItemName,
                        Price = source.Price,
                        Quantity = source.Quantity
                    };
                });
            return mockMapper;
        }

        private List<Basket> GetTestBasket()
        {
            return new List<Basket>
            {
                new Basket { CartId = "123", BasketItems = new List<BasketItem> {
                    new BasketItem { ItemId = "999", ItemName = "Test Item 1", Price = 10, Quantity = 10 },
                    new BasketItem { ItemId = "888", ItemName = "Test Item 2", Price = 20, Quantity = 5 } } },
                new Basket { CartId = "456", BasketItems = new List<BasketItem> {
                    new BasketItem { ItemId = "777", ItemName = "Test Item 3", Price = 1, Quantity = 20 } } }
            };
        }
    }
}
