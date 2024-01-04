using Market.DAL;
using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Response;
using Market.Service.Interfaces;

namespace Market.Service.Implementation;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _db;
    private readonly IUserService _userService;
    private readonly IOrderRepository _orderRepository;
    public CartService(IUserService userService, IOrderRepository orderRepository, ApplicationDbContext db)
    {
        _userService = userService;
        _orderRepository = orderRepository;
        _db = db;
    }
    public async Task<BaseResponse<User>> AddToCart(string token, int[] AssortmentsId, int studiaID)
    {
        try
        {
            var baseResponse = new BaseResponse<User>();
            var UserId = "1006"; //_userService.Verify(token).Issuer;
            if (UserId == null)
            {
                baseResponse.Description = "Отсутсвуют данные пользователя";
                baseResponse.StatusCode = StatusCode.NotFound;
                return baseResponse;
            }

            int IntUserID;
            int.TryParse(UserId, out IntUserID);
            var orderModel = new Order()
            {
                CustomerId = IntUserID,
                StudiaId = studiaID,
                   
                OrderDetails = new List<OrderDetails>()
            };
            
            foreach (var _assortmentId in AssortmentsId)
            {
                var orderDetailModel = new OrderDetails()
                {
                    AssortmentId = _assortmentId,
                    sum = 1
                };
                orderModel.OrderDetails.Add(orderDetailModel);
            }

            await _orderRepository.Create(orderModel);
            await _db.SaveChangesAsync();

            return baseResponse;
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