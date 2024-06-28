using System.ComponentModel.DataAnnotations;

namespace Domain.Publishing.Models.Commands.CustomerCommands;

public record SignUpCommand
{
    [Required]
    [RegularExpression(@"^(?=.*[A-Za-z]{3,})(?=.*\d)[A-Za-z\d]{4,}$", ErrorMessage = "El usuario debe contener al menos 3 letras y 1 número.")]
    public string Usuario { get; set; }
    
    [Required]
    [StringLength(int.MaxValue, MinimumLength = 4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres.")]
    public string Contraseña { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "El correo debe tener un formato válido.")]
    public string Correo { get; set; }
    public string? ImagenUsuario { get; set; }
    [Required]
    public bool IsArtisan { get; set; }
    public string? Role{ get; set; }
}

