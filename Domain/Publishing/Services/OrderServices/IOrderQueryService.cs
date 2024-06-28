using Domain.Publishing.Models.Queries.OrderQueries;
using Domain.Publishing.Models.Response.OrderResponse;

namespace Domain.Publishing.Services.OrderServices;

public interface IOrderQueryService
{
    Task<List<OrderResponse>?> Handle(GetAllOrdersQuery query);
    Task<OrderResponse> Handle(GetOrderByIdQuery query);
}