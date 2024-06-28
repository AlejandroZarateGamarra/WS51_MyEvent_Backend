namespace Domain.Publishing.Models.Response.OrderResponse;

public class OrderResponse
{
    public string ProductId { get; set; }
    public string Product { get; set; }
    public List<OrderParametersResponse> Parameters { get; set; }
    public decimal Price { get; set; }
}