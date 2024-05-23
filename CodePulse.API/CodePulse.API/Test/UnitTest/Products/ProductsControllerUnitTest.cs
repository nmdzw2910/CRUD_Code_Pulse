using AutoMapper;
using CodePulse.API.Controllers;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using CodePulse.API.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CodePulse.API.Test.UnitTest.Products
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            Mock<IProductService> mockProductService = new();
            _mockMapper = new Mock<IMapper>();
            _controller = new ProductsController(_mockProductRepository.Object, mockProductService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsNotFound_WhenNoProductsExist()
        {
            // Arrange
            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No products found.", actionResult.Value);
        }

        [Fact]
        public async Task GetAllProducts_ReturnsOk_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product> { new Product { Id = Guid.NewGuid(), Name = "Test Product" } };
            var productDtos = new List<ProductDto> { new ProductDto { Id = Guid.NewGuid(), Name = "Test Product" } };

            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ProductDto>>(products)).Returns(productDtos);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(productDtos, actionResult.Value);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Product with ID {productId} not found.", actionResult.Value);
        }

        [Fact]
        public async Task GetProductById_ReturnsOk_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Test Product" };
            var productDto = new ProductDto { Id = productId, Name = "Test Product" };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockMapper.Setup(mapper => mapper.Map<ProductDto>(product)).Returns(productDto);

            // Act
            var result = await _controller.GetProductById(productId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(productDto, actionResult.Value);
        }

        [Fact]
        public async Task DeleteProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            // Act
            var result = await _controller.DeleteProductById(productId);

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Product with ID {productId} not found.", actionResult.Value);
        }

        [Fact]
        public async Task BulkDeleteProducts_ReturnsBadRequest_WhenNoIdsProvided()
        {
            // Act
            var result = await _controller.BulkDeleteProducts(null);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No product IDs provided for deletion.", actionResult.Value);
        }
    }
}
