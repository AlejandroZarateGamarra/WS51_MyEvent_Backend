namespace Domain.Publishing.Models.Response;

public class ParametroResponse
{
    public string Nombre { get; set; }
    public List<ValorParametroResponse> Valores { get; set; }
}