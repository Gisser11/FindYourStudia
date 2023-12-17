namespace Market.Domain.Entity;

public class Order
{
    public int Id { get; set; }
    
    public int StudiaId { get; set; }
    public Studia Studia { get; set; }
    

    public int UserId { get; set; }
    public User User { get; set; }
    
    public int Sum { get; set; }
    
    public DateTime? DateTime { set; get; }
}