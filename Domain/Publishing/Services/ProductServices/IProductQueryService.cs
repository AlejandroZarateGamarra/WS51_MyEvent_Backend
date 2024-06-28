using Domain.Publishing.Models.Queries.ProductQueries;
using Domain.Publishing.Models.Response;

namespace Domain.Publishing.Services.ProductServices;

public interface IProductQueryService
{
    Task<List<ProductResponse>?> Handle(GetAllProductsQuery query);
    Task<ProductResponse> Handle(GetProductByIdQuery query);
    
}