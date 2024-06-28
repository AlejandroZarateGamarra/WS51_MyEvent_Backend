using Domain.Publishing.Services;

namespace Application.CommandServices.CustomerCommandService;

public class EncryptCommandService: IEncryptService
{
    public string Encrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    
    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
    
}