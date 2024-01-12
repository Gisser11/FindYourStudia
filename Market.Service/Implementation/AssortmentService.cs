using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.Assortments;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class AssortmentService: IAssortmentService
{
    private readonly IAssortmentRepository _assortmentRepository;

    public AssortmentService(IAssortmentRepository assortmentRepository)
    {
        _assortmentRepository = assortmentRepository;
    }

    public async Task<IBaseResponse<IEnumerable<Assortment>>> GetAssortmentList(int id)
    {
        var baseResponse = new BaseResponse<IEnumerable<Assortment>>();

        try
        {
            var response = await _assortmentRepository.GetAssortmentsWithKey(id);
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Data = response;
            baseResponse.Description = "GetAssortmentList (int id) successfull\n";
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<Assortment>>()
            {
                Description = "GetAssortmentList (int id) failed",
                StatusCode = StatusCode.NotFound
            };
        }
    }

    public async Task<IBaseResponse<Assortment>> CreateAssortment(AssortmentViewModel assortmentViewModel)
    {
        var baseResponse = new BaseResponse<Assortment>();

        try
        {
            var assortmentModel = new Assortment()
            {
                StudiaId = assortmentViewModel.StudiaId,
                Name = assortmentViewModel.Name,
                Price = assortmentViewModel.Price
            };
            var response = await _assortmentRepository.Create(assortmentModel);
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Description = "GetAssortmentList (int id) successfull\n";
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Assortment>()
            {
                Description = "GetAssortmentList (int id) failed",
                StatusCode = StatusCode.NotFound
            };
        }
    }

    public async Task<IBaseResponse<Assortment>> DeleteAssortment(int id)
    {
        var baseResponse = new BaseResponse<Assortment>();
        try
        {
            var response = await _assortmentRepository.Delete(id);
            
            baseResponse.Description = response == true
                ? "Удаление успешно"
                : "Произошла ошибка в удалении, скорее всего, нужный товар не найден";
            
            return baseResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IBaseResponse<Assortment>> UodateAssortment(AssortmentViewModel assortmentViewModel)
    {
        var baseResponse = new BaseResponse<Assortment>();

        try
        {
            var response = await _assortmentRepository.GetById(assortmentViewModel.Id);

            response.Id = assortmentViewModel.Id;
            response.Name = assortmentViewModel.Name;
            response.Price = assortmentViewModel.Price;

            await _assortmentRepository.Update(response);
            
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