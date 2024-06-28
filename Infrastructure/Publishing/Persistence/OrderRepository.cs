using Domain.Publishing.Models.Entities.Orders;
using Domain.Publishing.Repositories;
using Infrastructure.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Publishing.Persistence;

public class OrderRepository: IOrderRepository
{
    
    private readonly ArtisaniaDBContext _context;
    
    public OrderRepository(ArtisaniaDBContext artisaniaDBContext)
    {
        _context = artisaniaDBContext;
    }
    public async Task<int> SaveAsync(Order data)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                _context.Orders.Add(data);
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

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Parameters)
            .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Parameters)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        return order;
    }
    public async Task<int> GetOrderCountForProduct(string productId)
    {
        return await _context.Orders.CountAsync(o => o.ProductId == productId);
    }
    
    public async Task<int> GetRepeatedParameterValuesCount(string productId, List<OrderParameter> parameters)
    {
        var parameterValues = parameters.Select(p => p.ParamValue).ToList();
        return await _context.Orders
            .Where(o => o.ProductId == productId && o.Parameters.Any(p => parameterValues.Contains(p.ParamValue)))
            .CountAsync();
    }
}