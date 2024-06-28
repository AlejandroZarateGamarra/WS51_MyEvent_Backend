using Domain.Publishing.Repositories;

namespace Domain.Publishing.Services.ProductServices;

public class ProductServiceD
{
    private readonly IProductRepository _productRepository;

    public ProductServiceD(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task DeleteProduct(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException("Product not found");
        }

        await _productRepository.DeleteAsync(product.Id);
    }
}