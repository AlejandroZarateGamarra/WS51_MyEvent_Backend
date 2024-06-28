using AutoMapper;
using Domain.Publishing.Models.Entities;
using Domain.Publishing.Models.Queries.CustomerQueries;
using Domain.Publishing.Models.Response;
using Domain.Publishing.Repositories;
using Domain.Publishing.Services;

namespace Application.QueryServices.CustomerQueryService;

public class CustomerQueryService:ICustomerQueryService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    
    public CustomerQueryService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    
    public async Task<List<CustomerResponse>?> Handle(GetAllCustomersQuery query)
    {
        var data = await _customerRepository.GetAllAsync();
        var result = _mapper.Map<List<Customer>, List<CustomerResponse>>(data);
        return result;
    }
    
    public async Task<CustomerResponse?> Handle(GetCustomerByIdQuery query)
    {
        var data = await _customerRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<Customer, CustomerResponse>(data);
        return result;
    }
}