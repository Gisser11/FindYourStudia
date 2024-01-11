using Market.DAL.Interfaces.IServices;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class ManagmentService :IManagmentService
{
    private readonly IUserService _userService;
    private readonly IStudiaRepository _studiaRepository;
    private readonly IAdminService _adminService;

    public ManagmentService(IUserService userService, IStudiaRepository studiaRepository, IAdminService adminService)
    {
        _userService = userService;
        _studiaRepository = studiaRepository;
        _adminService = adminService;
    }
    
    /*
     * Каждый раз когда модератор создает менеджера, на своей странице
     * присходит автоматическая инициализация автосервиса, привязанного к менеджеру,
     * это работает путем верификации токена, по нему берется ID менеджера и
     * привязывается к автосервису.
     * В последующем, каждый раз когда менеджер будет заходить на страницу,
     * систему будет сверять ID менеджера с ManagerID в своей модели и отображать только те данные,
     * которые привязаны к текущему менеджеру. 
     */
    
    public async Task<IBaseResponse<Studia>> CreateStudia(StudiaViewModel studiaViewModel, string token)
    {
        try
        {
            var managerId = _userService.Verify(token).Issuer;
            
            studiaViewModel.ManagerId = int.Parse(managerId);
            
            var response = await _adminService.EditStudia(studiaViewModel);
            
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IBaseResponse<Studia>> GetById(string token)
    {
        var baseResponse = new BaseResponse<Studia?>();
        
        try
        {

            var managerId = int.Parse(_userService.Verify(token).Issuer);
            
            var response = _studiaRepository.GetById(managerId);

            baseResponse.Data = response;
            return baseResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}