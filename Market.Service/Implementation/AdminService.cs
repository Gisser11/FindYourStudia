using Market.DAL.Interfaces;
using Market.DAL.Interfaces.IServices;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Domain.ViewModels.User;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class AdminService :IAdminService
{
    #region InitializeRepo`s

    private readonly IUserRepository _userRepository;
    private readonly IStudiaRepository _studiaRepository;
    

    public AdminService(IUserRepository userRepository, IStudiaRepository studiaRepository)
    {
        _userRepository = userRepository;
        _studiaRepository = studiaRepository;
    }

    #endregion
    
    
    public async Task<IBaseResponse<Studia>> EditStudia(StudiaViewModel studiaViewModel)
    {
        var baseResponse = new BaseResponse<Studia>();
        try
        {
            if (studiaViewModel.Id == 0)
            {
                Random rnd = new Random();
                double value = rnd.NextDouble() * 3 + 2;
                
                var studia = new Studia
                {
                    Name = studiaViewModel.Name,
                    ManagerId = studiaViewModel.ManagerId,
                    City = studiaViewModel.City,
                    Description = studiaViewModel.Description,
                    Rating = Math.Round(value, 2),
                };

                await _studiaRepository.Create(studia);
                baseResponse.Description = "Создание автосервиса - успешно ";
            }
            
            //Редактирование существующего
            if (studiaViewModel.Id != 0)
            {
                var findedStudia = _studiaRepository.GetById(studiaViewModel.Id);

                findedStudia.Name = studiaViewModel.Name;
                findedStudia.Description = studiaViewModel.Description;
                findedStudia.City = studiaViewModel.City;

                await _studiaRepository.Update(findedStudia);

                baseResponse.Description = "Редактирование автосервиса - успешно ";
            }

            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Studia>
            {
                Description = $"[Создание / Редактирование автосервиса - ] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<Studia>> DeleteStudia(int id)
    {
        var baseResponse = new BaseResponse<Studia>();
        try
        {
            _studiaRepository.Delete(id);
            
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<Studia>
            {
                Description = $"Проблема в удалении - {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }


    #region UserServices
    public async Task<IBaseResponse<User>> EditUser(int id, UserViewModel model)
    {
        var baseResponse = new BaseResponse<User>();
        
        try
        {
            var user = _userRepository.GetById(id);
            
            if (user == null)
            {
                baseResponse.StatusCode = StatusCode.NotFound;
                baseResponse.Description = "User not found";
                return baseResponse;
            }

            // Update user properties from the model
            user.Name = model.Name;
            user.Email = model.Email;
            user.TypeUserRole = model.TypeUserRole;

            await _userRepository.Update(user);
        
            baseResponse.StatusCode = StatusCode.OK;
            baseResponse.Data = user; 

            return baseResponse;
        }
        catch (Exception ex)
        {
            // Log the exception for debugging
            // logger.LogError(ex, "Error occurred while editing user.");

            return new BaseResponse<User>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }

    public async Task<IBaseResponse<User>> DeleteUser(int id)
    {
        var baseResponse = new BaseResponse<User>();
        try
        {
            var response = _userRepository.Delete(id);
            baseResponse.Description = "Успешно";
            return baseResponse;
        }
        catch (Exception ex)
        {
            return new BaseResponse<User>()
            {
                Description = $"[Edit] : {ex.Message}",
                StatusCode = StatusCode.InternalServiceError
            };
        }
    }
    

    #endregion
}