namespace Domain.Publishing.Models.Commands.ProductCommands;

public class ParametroPersonalizacionCommand
{
    public List<ParametroCommand> Parametros { get; set; }
    public string Gravado { get; set; }
}