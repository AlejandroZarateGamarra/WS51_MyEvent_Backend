using System.Data;
using System.Net.Mime;
using Application.CommandServices.CustomerCommandService;
using AutoMapper;
using Domain.Publishing.Models.Commands.CustomerCommands;
using Domain.Publishing.Models.Entities;
using Domain.Publishing.Repositories;
using Domain.Publishing.Services;
using Moq;
using Xunit;
using WS51_MyEvent.Mapper;
using WS51_MyEvent.Middleware;
using WS51_MyEvent.Mapper;

namespace Domain.test;

public class CustomerUnitTest
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IEncryptService> _encryptServiceMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly CustomerCommandService _customerCommandService;

    public CustomerUnitTest()
    {
        _customerRepositoryMock = new Mock<ICustomerRepository>();
        _encryptServiceMock = new Mock<IEncryptService>();
        _tokenServiceMock = new Mock<ITokenService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RequestToModel>();
            cfg.AddProfile<ModelToResponse>();
            cfg.AddProfile<ModelToRequest>();
        });
        var mapper = config.CreateMapper();

        _customerCommandService = new CustomerCommandService(_customerRepositoryMock.Object, mapper, _encryptServiceMock.Object, _tokenServiceMock.Object);
        
    }

    [Fact]
    public async Task SignInCommand_ShouldReturnToken_WhenValidCredentialsAreProvided()
    {
        // Arrange
        var command = new SignInCommand
        {
            Correo = "test@example.com",
            Contraseña = "password"
        };

        var customer = new Customer
        {
            Id = 1,
            Correo = "test@example.com",
            ContraseñaHashed = "hashed_password"
        };

        _customerRepositoryMock.Setup(x => x.GetByEmail(command.Correo)).ReturnsAsync(customer);
        _encryptServiceMock.Setup(x => x.Verify(command.Contraseña, customer.ContraseñaHashed)).Returns(true);
        _tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<Customer>())).Returns("token");

        // Act
        var result = await _customerCommandService.Handle(command);

        // Assert
        Assert.Equal("token", result);
    }
    [Fact]
    public async Task SignUpCommand_ShouldThrowException_WhenEmailAlreadyExists()
    {
        // Arrange
        var command = new SignUpCommand
        {
            Usuario = "testUser",
            Correo = "existing@example.com",
            Contraseña = "password",
            IsArtisan = false,
            Role = "User"
        };

        var existingCustomer = new Customer
        {
            Id = 1,
            Usuario = "existingUser",
            Correo = "existing@example.com",
            ContraseñaHashed = "hashed_password"
        };

        _customerRepositoryMock.Setup(x => x.GetByEmail(command.Correo)).ReturnsAsync(existingCustomer);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<DuplicateNameException>(async () => await _customerCommandService.Handle(command));
        Assert.Equal("Usuario ya existe", exception.Message);
    }
}