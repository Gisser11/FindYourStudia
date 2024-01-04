using Market.Domain.ViewModels;
using Market.Domain.ViewModels.Orders;
using Market.Service.Implementation;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("[controller]")]
public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [Route("addToCart/${studiaID}")]
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] OrderViewModel orderViewModel, int studiaID)
    {
        var value = Request.Cookies["token"];
        if (value != null)
        {
            var response = await _cartService.AddToCart(value, orderViewModel.AssortmentIDs, studiaID);
            
            return Ok();
        }
        return BadRequest("Для оформления нужно авторизоваться");    
    }
}