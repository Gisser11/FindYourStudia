using Market.DAL.Interfaces;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Domain.ViewModels.User;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("[controller]")]
public class AdminController : Controller
{
    
    private readonly IAdminService _adminService;
    private readonly IStudiaService _studiaService;
    private readonly IAssortmentService _assortmentService;
    private readonly IUserService _userService;

    public AdminController (IAdminService adminService, IStudiaService studiaService, IAssortmentService assortmentService, IUserService userService)
    {
        _adminService = adminService;
        _studiaService = studiaService;
        _assortmentService = assortmentService;
        _userService = userService;
    }
    
    /*
     * У нас две ключевые роли: Manager, Moderator
     * В зависимости, какая роль у пользователя, такой будет и функционал
     * Manager - свой конкретно привязанный автосервис
     * Moderator - доступен абсолютно весь функционал.
     */
    
    #region Manager
    
    

    #endregion
    
    [Route("StudiaPage")]
    public IActionResult StudiaPage()
    {
        return View();
    }
    
    [Route("UserPage")]
    public IActionResult UserPage()
    {
        return View();
    }

    #region STUDIA CRUD
    [Route("CreateOrUpdateStudia")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] StudiaViewModel studiaViewModel)
    {
        var response = await _adminService.EditStudia(studiaViewModel);
        
        return Json(response);
    }

    [HttpGet]
    [Route("GetAssortment/{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var response = await _assortmentService.GetAssortmentList(id);
        
        return Ok(response.Data);
    }
    
    
    [HttpDelete]
    [Route("DeleteStudiaId/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var response = await _adminService.DeleteStudia(id);
        
        return Json(response);
    }
    
    #endregion
    
    
    #region USER CRUD
    [HttpDelete]
    [Route("DeleteUserId/{Id:int}")]
    public IActionResult DeleteUser([FromRoute] int Id)
    {
        var response = _adminService.DeleteUser(Id);
        return Ok(response);
    }
    
    [HttpPost]
    [Route("CreateOrUpdateUser")]
    public async Task<IActionResult> Create([FromBody] UserRegisterViewModel dto)
    {
        dto.TypeUserRole = "true";
        var response = await _userService.RegisterUser(dto);
        
        return Json(response);
    }

    
    [HttpPut]
    [Route("EditUserName")]
    public IActionResult EditNode([FromBody] UserViewModel dto)
    {
        var response = _adminService.EditUser(dto.Id, dto);
        return Ok(response);
    }
    #endregion
    
    
}