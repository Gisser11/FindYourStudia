namespace Market.Domain.Entity;

public class OrderDetails
{
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; } // Связь с заказом
    
    public int AssortmentId { get; set; } 
    public Assortment Assortment { get; set; } 
    
    public int sum { get; set; }
}