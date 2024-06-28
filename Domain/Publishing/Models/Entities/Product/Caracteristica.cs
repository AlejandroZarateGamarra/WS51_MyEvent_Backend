namespace Domain.Publishing.Models.Entities.Product;

public class Caracteristica:ModelBase
{
    public string? Nombre { get; set; }
    public string? Valor { get; set; }
    public int ProductId { get; set; }
}