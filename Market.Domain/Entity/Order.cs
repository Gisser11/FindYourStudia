namespace Market.Domain.Entity;

public class Order
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    
    public User Customer { get; set; } 
    
    
    public int StudiaId { get; set; }
    
    public Studia Studia { get; set; } 
    
    
    public List<OrderDetails> OrderDetails { get; set; } 
}