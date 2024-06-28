using AutoMapper;
using Domain.Publishing.Models.Entities.Product;
using Domain.Publishing.Models.Queries.ProductQueries;
using Domain.Publishing.Models.Response;
using Domain.Publishing.Repositories;
using Domain.Publishing.Services.ProductServices;

namespace Application.QueryServices.ProductQueryService;

public class ProductQueryServices:IProductQueryService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public ProductQueryServices(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<List<ProductResponse>?> Handle(GetAllProductsQuery query)
    {
        var data = await _productRepository.GetAllAsync();
        var result = _mapper.Map<List<Product>, List<ProductResponse>>(data);
        return result;
    }

    public async Task<ProductResponse> Handle(GetProductByIdQuery query)
    {
        var data = await _productRepository.GetByIdAsync(query.Id);
        var result = _mapper.Map<Product, ProductResponse>(data);
        return result;
    }
}