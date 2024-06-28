using AutoMapper;
using Domain.Publishing.Models.Entities.Orders;
using Domain.Publishing.Models.Queries.OrderQueries;
using Domain.Publishing.Models.Response.OrderResponse;
using Domain.Publishing.Repositories;
using Domain.Publishing.Services.OrderServices;

namespace Application.QueryServices.OrderQueryService;

public class OrderQueryService: IOrderQueryService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderQueryService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    
    public async Task<List<OrderResponse>?> Handle(GetAllOrdersQuery query)
    {
        var data = await _orderRepository.GetAllAsync();
        var result = _mapper.Map<List<Order>, List<OrderResponse>>(data);
        return result;
    }
    
    public async Task<OrderResponse> Handle(GetOrderByIdQuery query)
    {
        var data = await _orderRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<Order, OrderResponse>(data);
        return result;
    }
}