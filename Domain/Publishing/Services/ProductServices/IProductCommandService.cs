using System.Threading.Tasks;
using Domain.Publishing.Models.Commands.ProductCommands;

namespace Domain.Publishing.Services.ProductServices
{
    public interface IProductCommandService
    {
        Task<int> Handle(CreateProductCommand command);
        Task<bool> DeleteAsync(int id);
    }
}