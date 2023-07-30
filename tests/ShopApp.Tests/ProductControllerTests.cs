using Moq;
using NUnit.Framework;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Controllers;
using AutoFixture;
using ShopApp.Domain.DTOs.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task GetProductById_ReturnOk()
        {
            //Arrange
            var expectedProductResponseDTO = _fixture.Create<GetProductResponseDto>();
            var productId = 1;

            _productServiceMock.Setup(service => service.GetById(productId))
                .ReturnsAsync(expectedProductResponseDTO);

            //Act
            var actionResult = await _productController.GetById(productId);

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var okResult = (OkObjectResult)actionResult.Result;
            Assert.AreEqual(expectedProductResponseDTO, okResult.Value);
        }

        [Test]
        public async Task GetProductById_NonExistent_ReturnNotFound()
        {
            //Arrange
            var nonExistentProductId = 99;
            _productServiceMock.Setup(service => service.GetById(nonExistentProductId))
                .ReturnsAsync(null as GetProductResponseDto);

            //Act
            var actionResult = await _productController.GetById(nonExistentProductId);

            //Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(actionResult.Result);
        }

        [Test]
        public async Task AddProduct_UserAuthorized_ReturnOk() 
        {
            //Arrange
            _authorizationServiceMock.Setup(service => service.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AddProductRequestDto>(), "StorePolicy"))
                .ReturnsAsync(AuthorizationResult.Success());
            _productServiceMock.Setup(service => service.Add(It.IsAny<AddProductRequestDto>()))
                .ReturnsAsync(It.IsAny<GetProductResponseDto>());

            //Act
            var actionResult = await _productController.Add(It.IsAny<AddProductRequestDto>());

            //Assert
            var okResult = (OkObjectResult)actionResult.Result;
            Assert.IsInstanceOf<OkObjectResult>(okResult);
            Assert.AreEqual(It.IsAny<GetProductResponseDto>(), okResult.Value);
        }

        [Test]
        public async Task AddProduct_UserUnauthorized_ReturnForbid()
        {
            //Arrange
            _authorizationServiceMock.Setup(service => service.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<AddProductRequestDto>(), "StorePolicy"))
                .ReturnsAsync(AuthorizationResult.Failed());
            _productServiceMock.Setup(service => service.Add(It.IsAny<AddProductRequestDto>()))
                .ReturnsAsync(It.IsAny<GetProductResponseDto>());

            //Act
            var actionResult = await _productController.Add(It.IsAny<AddProductRequestDto>());

            //Assert
            var forbidResult = actionResult.Result;
            Assert.IsInstanceOf<ForbidResult>(actionResult.Result);
        }
    }
}