using Moq;
using NUnit.Framework;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Controllers;
using AutoFixture;
using ShopApp.Domain.DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShopApp.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> _productServiceMock;
        private Mock<IAuthorizationService> _authorizationServiceMock;
        private Fixture _fixture;
        private ProductController _productController;        

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _productServiceMock = new Mock<IProductService>();
            _authorizationServiceMock = new Mock<IAuthorizationService>();
            _productController = new ProductController(_productServiceMock.Object, _authorizationServiceMock.Object);
        }

        [Test]
        public async Task Get_ProductById_ReturnOk()
        {
            //Arrange
            var expectedProductResponseDTO = _fixture.Create<GetProductResponseDto>();
            var productId = 1;

            _productServiceMock.Setup(service => service.GetById(productId)).ReturnsAsync(expectedProductResponseDTO);

            //Act
            var actionResult = await _productController.GetById(productId);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var okResult = (OkObjectResult)actionResult.Result;
            Assert.AreEqual(expectedProductResponseDTO, okResult.Value);
        }

        [Test]
        public async Task Get_ProductById_NonExistent_ReturnNotFound()
        {
            //Arrange
            var nonExistentProductId = 99;
            _productServiceMock.Setup(service => service.GetById(nonExistentProductId)).ReturnsAsync(null as GetProductResponseDto);

            //Act
            var actionResult = await _productController.GetById(nonExistentProductId);

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        } 
    }
}