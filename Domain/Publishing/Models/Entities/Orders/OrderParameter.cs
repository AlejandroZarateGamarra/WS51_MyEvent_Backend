namespace Domain.Publishing.Models.Entities.Orders;

public class OrderParameter
{
    public int Id { get; set; }
    public string ParamName { get; set; }
    public string ParamValue { get; set; }
    public int OrderId { get; set; }
}