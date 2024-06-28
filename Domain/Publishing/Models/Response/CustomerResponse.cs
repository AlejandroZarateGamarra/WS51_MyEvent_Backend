namespace Domain.Publishing.Models.Response;

public class CustomerResponse
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string ContraseñaHashed { get; set; }
    public string Correo { get; set; }
    public string ImagenUsuario { get; set; }
    public bool IsArtisan { get; set; }
    public string Role { get; set; }
}