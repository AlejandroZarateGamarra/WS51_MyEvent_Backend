using System.ComponentModel.DataAnnotations;

namespace Domain.Publishing.Models.Commands.CustomerCommands;

public record SignInCommand
{
    public string Correo { get; set; }
    public string Contraseña { get; set; }
}