namespace Domain.Publishing.Models.Entities.Product;

public class Parametro:ModelBase
{

    public string Nombre { get; set; }
    public List<ValorParametro> Valores { get; set; }
    public int ParametrosPersonalizacionId { get; set; }
}