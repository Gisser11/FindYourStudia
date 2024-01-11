using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.StudiaViewModel;

namespace Market.Service.Interfaces;

public interface IManagmentService
{
    Task<IBaseResponse<Studia>> CreateStudia(StudiaViewModel studiaViewModel, string token);
    
    Task<IBaseResponse<Studia>> GetById(string token);
    
}