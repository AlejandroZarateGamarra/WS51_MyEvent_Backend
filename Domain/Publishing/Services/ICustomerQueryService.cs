using Domain.Publishing.Models.Queries.CustomerQueries;
using Domain.Publishing.Models.Response;

namespace Domain.Publishing.Services;

public interface ICustomerQueryService
{
    Task<List<CustomerResponse>?> Handle(GetAllCustomersQuery query);
    Task<CustomerResponse> Handle(GetCustomerByIdQuery query);
}