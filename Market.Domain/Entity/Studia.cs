using Market.Domain.Enum.Studia;

namespace Market.Domain.Entity;

public class Studia
{
    public int Id { get; set; }
    
    public int ManagerId { get; set; }
    public string Name { get; set; }
    
    public string City { get; set; }
    
    public string Description { get; set; }
    
    public double Rating { get; set; }
    
    public List<Assortment> Assortments { get; set; }
}