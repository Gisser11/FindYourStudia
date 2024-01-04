using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Response;

namespace Market.Service.Interfaces;

public interface ICartService
{
    public Task<BaseResponse<User>> AddToCart(string token, int[] AssortmentsId, int studiaID);
}