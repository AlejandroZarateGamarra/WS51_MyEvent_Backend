using Domain.Publishing.Models.Entities.Product;

namespace Domain.Publishing.Models.Response;

public class ProductResponse
{
    public string Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string Detalles { get; set; }
    public string DetallesDelArtesano { get; set; }
    public ParametrosPersonalizacionResponse? ParametrosPersonalizacion { get; set; }
    public int Tamaño { get; set; }
    public string InputText { get; set; }
    public string Gravado { get; set; }
    public string Categoria { get; set; }
    public string Imagen { get; set; }
    public List<ImagenResponse>? ImagenesDetalle { get; set; }
    public string Autor { get; set; }
    public List<CaracteristicaResponse> Caracteristicas { get; set; }
}