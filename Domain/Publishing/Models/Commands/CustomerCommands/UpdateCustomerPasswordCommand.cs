using System.ComponentModel.DataAnnotations;

namespace Domain.Publishing.Models.Commands.CustomerCommands;

public class UpdateCustomerPasswordCommand
{
    [Required]public int Id{ get; set; }
    [Required]public string Contraseña { get; set; }
}