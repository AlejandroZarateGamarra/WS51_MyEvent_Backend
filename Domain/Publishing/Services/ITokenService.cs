using Domain.Publishing.Models.Entities;

namespace Domain.Publishing.Services;

public interface ITokenService
{
    string GenerateToken(Customer user);
    
    Task<int?> ValidateToken(string token);
}