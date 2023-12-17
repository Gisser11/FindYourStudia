using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class CartService : ICartService
{

    public async Task<BaseResponse<User>> AddToCart(int UserId, int AssortmentId, int StudiaId, int count)
    {
        try
        {
            var response = new BaseResponse<User>();
            return response;
        }
        catch (Exception e)
        {
            return new BaseResponse<User>()
            {
                Description = "Произошла ошибка",
                StatusCode = StatusCode.NotFound
            };
        }
    }
}