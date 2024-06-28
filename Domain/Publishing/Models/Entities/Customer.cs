namespace Domain.Publishing.Models.Entities;

public class Customer : ModelBase
{
    public string Usuario { get; set; }
    public string ContraseñaHashed { get; set; }
    public string Correo { get; set; }
    
    private string _imagenUsuario;
    public string ImagenUsuario 
    {
        get { return _imagenUsuario ?? "default.jpg"; }
        set { _imagenUsuario = value; }
    }
    
    public bool IsArtisan { get; set; }
    private string _role;
    public string Role
    {
        get { return _role ?? "User"; }
        set { _role = value; }
    }
    
}