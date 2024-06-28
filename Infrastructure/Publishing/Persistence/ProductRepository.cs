using Domain.Publishing.Models.Entities.Product;
using Domain.Publishing.Repositories;
using Infrastructure.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Publishing.Persistence;

public class ProductRepository: IProductRepository
{
    private readonly ArtisaniaDBContext _context;
    
    public ProductRepository(ArtisaniaDBContext artisaniaDBContext)
    {
        _context = artisaniaDBContext;
    }

    public  async Task<int> SaveAsync(Product data)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                _context.Products.Add(data);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        return data.Id;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(p => p.ParametrosPersonalizacion)
            .ThenInclude(pp => pp.Parametros)
            .ThenInclude(p => p.Valores)
            .Include(p => p.ImagenesDetalle)
            .Include(p => p.Caracteristicas)
            .ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.ParametrosPersonalizacion)
            .ThenInclude(pp => pp.Parametros)
            .ThenInclude(p => p.Valores)
            .Include(p => p.ImagenesDetalle)
            .Include(p => p.Caracteristicas)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Product> GetByNameAsync(string name)
    {
        return await _context.Products
            .Include(p => p.ParametrosPersonalizacion)
            .ThenInclude(pp => pp.Parametros)
            .ThenInclude(p => p.Valores)
            .Include(p => p.ImagenesDetalle)
            .Include(p => p.Caracteristicas)
            .FirstOrDefaultAsync(p => p.Nombre == name);
    }
}