using System.ComponentModel.DataAnnotations;

namespace Domain.Publishing.Models.Commands.CustomerCommands;

public class UpdateCustomerCommand
{
    [Required]public int Id { get; set; }
    [Required]public string Usuario { get; set; }
    [Required]public string Correo { get; set; }
    [Required]public string ImagenUsuario { get; set; }
}