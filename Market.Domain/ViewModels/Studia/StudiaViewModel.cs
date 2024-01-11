using System.Text.Json.Serialization;
using Market.Domain.Enum.Studia;

namespace Market.Domain.ViewModels.StudiaViewModel;

public class StudiaViewModel
{
    public int Id { get; set; }

    [JsonPropertyName("Name")] 
    public string Name { get; set; }
    
    [JsonPropertyName("ManagerId")]
    public int ManagerId { get; set; }

    [JsonPropertyName("City")] 
    public string City { get; set; }
    
    [JsonPropertyName("Description")] 
    public string Description { get; set; }
    
}