using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Assortments;

namespace Market.Service.Interfaces;

public interface IAssortmentService
{
    Task<IBaseResponse<IEnumerable<Assortment>>> GetAssortmentList(int id);
    
    Task<IBaseResponse<Assortment>> CreateAssortment(AssortmentViewModel assortmentViewModel);
}