namespace Domain.Publishing.Services;

public interface IEncryptService
{
    string Encrypt(string password);
    bool Verify(string password, string passwordHash);
}