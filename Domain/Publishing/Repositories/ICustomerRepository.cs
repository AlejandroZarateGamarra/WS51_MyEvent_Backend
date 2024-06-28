using Domain.Publishing.Models.Entities;

namespace Domain.Publishing.Repositories;

public interface ICustomerRepository
{
    Task<int> SaveAsync(Customer data);
    Task<List<Customer>> GetAllAsync();
    Task<Customer> GetByIdAsync(int id);
    Task<bool> Update(Customer data, int id);
    Task<bool> UpdatePassword(Customer data, int id);


    Task<Customer> Register(Customer customer);
    
    Task<Customer> Login(string email, string password);
    
    Task<Customer> GetByEmail(string email);
    
    Task Update(Customer customer);
    Task Delete(int id);
}