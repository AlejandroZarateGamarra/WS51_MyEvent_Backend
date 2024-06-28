using Application.CommandServices.OrderCommandService;
using AutoMapper;
using Domain.Publishing.Models.Commands.OrderCommands;
using Domain.Publishing.Models.Entities.Orders;
using Domain.Publishing.Models.Entities.Product;
using Domain.Publishing.Repositories;
using Moq;
using WS51_MyEvent.Mapper;
using Xunit;

namespace Domain.test
{
    public class OrderUnitTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly OrderCommandService _orderCommandService;

        public OrderUnitTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _orderRepositoryMock = new Mock<IOrderRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RequestToModel>();
                // Add your other AutoMapper profiles here
            });
            var mapper = config.CreateMapper();

            _orderCommandService = new OrderCommandService(_orderRepositoryMock.Object, mapper, _productRepositoryMock.Object);
        }
        [Fact]
        public async Task CreateOrderCommand_ShouldIncreasePrice_WhenProductHasBeenOrderedBefore()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                ProductId = "1",
                Price = 100m
            };

            var product = new Product
            {
                Id = 1,
                Precio = 100m,
                
            };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _orderRepositoryMock.Setup(x => x.GetOrderCountForProduct(command.ProductId)).ReturnsAsync(2);

            // Act
            var result = await _orderCommandService.Handle(command);

            // Assert
            Assert.Equal(100m + 100m * Shared.GlobalConstants.AUGMENT_PERCENT* 2, command.Price);
        }
        [Fact]
        public async Task Handle_ShouldIncreasePrice_WhenParameterValuesAreRepeated()
        {
            // Arrange
            _orderRepositoryMock.Setup(repo => repo.GetRepeatedParameterValuesCount(It.IsAny<string>(), It.IsAny<List<OrderParameter>>()))
                .ReturnsAsync(4);

            var command = new CreateOrderCommand
            {
                ProductId = "test",
                Price = 100,
                Parameters = new List<OrderParameterCommand>
                {
                    new OrderParameterCommand { ParamName = "test", ParamValue = "test" }
                }
            };

            // Act
            await _orderCommandService.Handle(command);

            // Assert
            Assert.Equal(105, command.Price);
        }
    }
}