using Domain.Publishing.Models.Commands.OrderCommands;
using Domain.Publishing.Models.Commands.ProductCommands;

namespace Domain.Publishing.Services.OrderServices;

public interface IOrderCommandService
{
    Task<int> Handle(CreateOrderCommand command);

}