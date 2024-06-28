namespace Domain.Publishing.Models.Entities.Orders;

public class Order:ModelBase
{
    public string ProductId { get; set; }
    public string Product { get; set; }
    public List<OrderParameter> Parameters { get; set; }
    public decimal Price { get; set; }
}