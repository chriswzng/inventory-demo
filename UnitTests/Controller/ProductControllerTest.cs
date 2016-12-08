using Inventory.Models;
using Moq;
using System.Linq;
using System.Collections.Generic;
using System;
using Xunit;
using Inventory.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.UnitTests
{
    public class ProductControllerTest
    {
        private Mock<IProductRepository> _mockProductRepository;
        private ProductController _productController;

        public ProductControllerTest()
        {
            this._mockProductRepository = new Mock<IProductRepository>();
            this._productController = new ProductController(this._mockProductRepository.Object);
        }

        [Fact]
        public void Should_SuccesfullyReturnAllProducts_When_GettingAllProducts()
        {
            // arrange
            Guid applePieGuid = Guid.NewGuid();
            Guid waterBottleGuid = Guid.NewGuid();
            Guid laptopGuid = Guid.NewGuid();

            ProductResultDTO applePie = new ProductResultDTO()
            {
                Id = applePieGuid, Name = "Apple Pie", CostPrice = 2.5, SellingPrice = 3.5
            };
            
            ProductResultDTO waterBottle = new ProductResultDTO()
            {
                Id = waterBottleGuid, Name = "Water Bottle", CostPrice = 10, SellingPrice = 28
            };

            ProductResultDTO laptop = new ProductResultDTO()
            {
                Id = laptopGuid, Name = "Dell Ultrabook X13", CostPrice = 3500, SellingPrice = 4500
            };

            this._mockProductRepository.Setup(r => r.GetAll()).Returns(new List<ProductResultDTO>()
            {
                applePie,waterBottle,laptop
            });

            // act
            var productList = this._productController.Get(null) as List<ProductResultDTO>;
            ProductResultDTO applePieResult = productList.Where(p => p.Id == applePieGuid).FirstOrDefault();
            ProductResultDTO waterBottleResult = productList.Where(p => p.Id == waterBottleGuid).FirstOrDefault();
            ProductResultDTO laptopResult = productList.Where(p => p.Id == laptopGuid).FirstOrDefault();

            // assert
            Assert.Equal(3, productList.Count);
            Assert.NotNull(applePieResult);
            Assert.NotNull(waterBottleResult);
            Assert.NotNull(laptopResult);
            Assert.Equal(applePie, applePieResult);
            Assert.Equal(waterBottle, waterBottleResult);
            Assert.Equal(laptop, laptopResult);
        }

        [Fact]
        public void Should_ReturnCorrectProduct_When_GettingProductById()
        {
            // arrange
            Guid existingProductId = Guid.NewGuid();
            ProductResultDTO existingProduct = new ProductResultDTO() 
            {
                Id = existingProductId, Name = "Apple Pie", Image = "apple_pie.jpg", CostPrice = 2.50, SellingPrice = 3.50
            };

            this._mockProductRepository.Setup(r => r.Get(existingProductId)).Returns(existingProduct);

            // act
            var result = this._productController.GetById(existingProductId) as ObjectResult;
            var productFound = result.Value as ProductResultDTO;

            // assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(productFound);
            Assert.Equal(existingProductId, productFound.Id);
            Assert.Equal(existingProduct.Name, productFound.Name);
            Assert.Equal(existingProduct.Image, productFound.Image);
            Assert.Equal(existingProduct.CostPrice, productFound.CostPrice);
            Assert.Equal(existingProduct.SellingPrice, productFound.SellingPrice);
        }

        [Fact]
        public void Should_ReturnProductNotFound_When_GettingProductWithIdThatDoesNotExist()
        {
            // arrange
            Guid nonExistingProductId = Guid.NewGuid();
            this._mockProductRepository.Setup(r => r.Get(nonExistingProductId)).Returns<ProductDTO>(null);

            // act
            var result = this._productController.GetById(nonExistingProductId) as ObjectResult;

            // assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }
    }
}