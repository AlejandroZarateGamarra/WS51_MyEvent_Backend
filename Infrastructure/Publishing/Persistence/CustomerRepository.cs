using Domain.Publishing.Models.Entities;
using Domain.Publishing.Repositories;
using Infrastructure.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Publishing.Persistence;

public class CustomerRepository:ICustomerRepository
{
    private readonly ArtisaniaDBContext _context;
    
    public CustomerRepository(ArtisaniaDBContext artisaniaDBContext)
    {
        _context = artisaniaDBContext;
    }
    public async Task<List<Customer>>GetAllAsync()
    {
        var result = await _context.Customers.ToListAsync();

        return result;
    }
    
    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
    }
    
    public async Task<int> SaveAsync(Customer data)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                _context.Customers.Add(data);
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
    
    public async Task<bool> Update(Customer data, int id)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var customerToUpdate = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
                customerToUpdate.Usuario = data.Usuario;
                customerToUpdate.ContraseñaHashed = data.ContraseñaHashed;
                customerToUpdate.Correo = data.Correo;
                customerToUpdate.ImagenUsuario = data.ImagenUsuario;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        return true;
    }
    
    public async Task<bool> UpdatePassword(Customer data, int id)
    {
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                var customerToUpdate = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
                customerToUpdate.ContraseñaHashed = data.ContraseñaHashed;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        return true;
    }

    /// <summary>

    public async Task<Customer> Register(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer> Login(string email, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> GetByEmail(string email)
    {
        var customer = await _context.Customers.Where(c => c.Correo == email).FirstOrDefaultAsync();
        return customer;
    }

    public async Task Update(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}