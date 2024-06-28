using System.ComponentModel.DataAnnotations;

namespace Domain.Publishing.Models.Commands.OrderCommands;

public class CreateOrderCommand
{
    [Required]
    public string ProductId { get; set; }
    public string Product { get; set; }
    public List<OrderParameterCommand> Parameters { get; set; }
    [Required]
    [Range(5, 250, ErrorMessage = "El precio debe ser mayor que 5 y menor que 250.")]
    public decimal Price { get; set; }
}