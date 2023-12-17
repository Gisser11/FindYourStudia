using Market.Domain.Entity;

public class Service
{
    public int Id { get; set; }

    public int StudiaId { get; set; } 

    public string Name { get; set; }
    
    public string Description { get; set; }

    public int Price { get; set; }
    public Studia Studia { get; set; } 

}