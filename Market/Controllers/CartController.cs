using Market.Domain.ViewModels;
using Market.Service.Implementation;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("api/[controller]")]
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IUserService _userService;

    public CartController(ICartService cartService, IUserService userService)
    {
        _cartService = cartService;
        _userService = userService;
    }

    [Route("addToCart")]
    [HttpPost]
    public async Task<IActionResult> AddToCart(ResponseToCartViewModel dto)
    {
        if (Request.Cookies["token"] != null)
        {
            var value = Request.Cookies["token"];
            var UserId = _userService.Verify(value).Issuer; // с токена

            dto.UserId = int.Parse(_userService.Verify(value).Issuer);
            
            return Ok();
        }

        return BadRequest("Отсутсвует токен");    
    }
}