namespace Domain.Publishing.Models.Entities.Product;

public class ParametrosPersonalizacion:ModelBase
{ 
    public List<Parametro> Parametros { get; set; }
    public string Gravado { get; set; }
    public int ProductId { get; set; } 
    
}