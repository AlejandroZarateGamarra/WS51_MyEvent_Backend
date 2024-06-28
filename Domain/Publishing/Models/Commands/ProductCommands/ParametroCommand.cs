using Domain.Publishing.Models.Entities.Product;

namespace Domain.Publishing.Models.Commands.ProductCommands;

public class ParametroCommand
{
    public string Nombre { get; set; }
    public List<ValorParametroCommand> Valores { get; set; }
}