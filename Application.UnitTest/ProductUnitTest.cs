using Xunit;
using Moq;
using System.Threading.Tasks;
using Domain.Publishing.Models.Commands.ProductCommands;
using Domain.Publishing.Models.Entities.Product;
using Domain.Publishing.Repositories;
using Application.CommandServices.ProductCommandService;
using AutoMapper;
using WS51_MyEvent.Mapper;

namespace Domain.test
{
    public class ProductUnitTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductCommandService _productCommandService;

        public ProductUnitTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RequestToModel>();
                // Add your other AutoMapper profiles here
            });
            var mapper = config.CreateMapper();

            _productCommandService = new ProductCommandService(_productRepositoryMock.Object, mapper);
        }

        [Fact]
        public async Task CreateProductCommand_ShouldThrowException_WhenProductNameExists()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Nombre = "existingProduct"
            };

            var existingProduct = new Product
            {
                Id = 1,
                Nombre = "existingProduct"
            };

            _productRepositoryMock.Setup(x => x.GetByNameAsync(command.Nombre)).ReturnsAsync(existingProduct);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _productCommandService.Handle(command));
            Assert.Equal("Ya existe un producto con el mismo nombre.", exception.Message);
        }

        [Fact]
        public async Task CreateProductCommand_ShouldCreateProduct_WhenProductNameIsNew()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Nombre = "newProduct"
            };

            _productRepositoryMock.Setup(x => x.GetByNameAsync(command.Nombre)).ReturnsAsync((Product)null);
            _productRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<Product>())).ReturnsAsync(1);

            // Act
            var result = await _productCommandService.Handle(command);

            // Assert
            Assert.Equal(1, result);
        }
    }
}