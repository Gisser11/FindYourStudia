using Market.DAL.Interfaces;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudiaController : Controller
{
    private readonly IStudiaService _studiaService;
    
    public StudiaController(IStudiaService studiaService)
    {
        _studiaService = studiaService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _studiaService.GetAllStudia(); 
        
        if (response.StatusCode == Domain.Enum.StatusCode.OK) 
            return Ok(response.Data);
        
        return Ok("Не найдено записей");
    }
}