using Domain.Publishing.Models.Entities.Product;

namespace Domain.Publishing.Repositories;

public interface IProductRepository
{
    Task<int> SaveAsync(Product data);
    Task<List<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<Product> GetByNameAsync(string name);

}