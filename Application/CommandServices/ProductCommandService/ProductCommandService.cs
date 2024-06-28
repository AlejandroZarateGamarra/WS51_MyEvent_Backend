using AutoMapper;
using Domain.Publishing.Models.Commands.ProductCommands;
using Domain.Publishing.Models.Entities.Product;
using Domain.Publishing.Repositories;
using Domain.Publishing.Services.ProductServices;

namespace Application.CommandServices.ProductCommandService;

public class ProductCommandService:IProductCommandService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public ProductCommandService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<int> Handle(CreateProductCommand command)
    {
        var productName = command.Nombre;

        var existingProduct = await _productRepository.GetByNameAsync(productName);
        if (existingProduct != null)
        {
            throw new Exception("Ya existe un producto con el mismo nombre.");
        }

        var product = _mapper.Map<CreateProductCommand, Product>(command);
        return await _productRepository.SaveAsync(product);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(product.Id);
        return true;
    }
}