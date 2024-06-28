namespace Domain.Publishing.Models.Entities.Product;

public class Product:ModelBase
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Precio { get; set; }
    public string? Detalles { get; set; }
    public string? DetallesDelArtesano { get; set; } 
    public ParametrosPersonalizacion? ParametrosPersonalizacion { get; set; }
    public int Tamaño { get; set; }
    public string? InputText { get; set; }
    public string? Gravado { get; set; }
    public string? Categoria { get; set; }
    public string? Imagen { get; set; }
    public List<Imagen>? ImagenesDetalle { get; set; }
    public string? Autor { get; set; }
    public List<Caracteristica> Caracteristicas { get; set; }
}