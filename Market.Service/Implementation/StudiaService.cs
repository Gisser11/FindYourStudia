using Market.DAL.Interfaces.IServices;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class StudiaService : IStudiaService
{
    private readonly IStudiaRepository _studiaRepository;

    public StudiaService(IStudiaRepository studiaService)
    {
        _studiaRepository = studiaService;
    }

    public async Task<IBaseResponse<IEnumerable<Studia>>> GetAllStudia()
    {
        var baseResponse = new BaseResponse<IEnumerable<Studia>>();
        
        try
        {
            var StudiaList = await _studiaRepository.Select();

            if (StudiaList.Count == 0)
            {
                baseResponse.Description = "Найдено 0 элементов";
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            
            baseResponse.Data = StudiaList;
            baseResponse.StatusCode = StatusCode.OK;
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Studia>>
            {
                Description = $"[GetCars] : {ex.Message}"
            };
        }
    }

    
    
   
}