using Market.DAL.Interfaces;
using Market.Domain.ViewModels.Assortments;
using Market.Domain.ViewModels.StudiaViewModel;
using Market.Domain.ViewModels.User;
using Market.Service.Interfaces;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers;

[Route("[controller]")]
public class AdminController : Controller
{
    
    private readonly IAdminService _adminService;
    private readonly IManagmentService _managmentService;
    private readonly IAssortmentService _assortmentService;
    private readonly IUserService _userService;

    public AdminController (IAdminService adminService, IAssortmentService assortmentService, IUserService userService, IManagmentService managmentService)
    {
        _adminService = adminService;
        _assortmentService = assortmentService;
        _userService = userService;
        _managmentService = managmentService;
    }
    
    /*
     * У нас две ключевые роли: Manager, Moderator
     * В зависимости, какая роль у пользователя, такой будет и функционал
     * Manager - свой конкретно привязанный автосервис
     * Moderator - доступен абсолютно весь функционал.
     */
    #region Manager
    
    /*[HttpGet]
    [Route("InitializeStudia")]
    public async Task<IActionResult> InitializeStudia()
    {
        if (Request.Cookies["token"] != null)
        {
            string requestCookie = Request.Cookies["token"];
            var response = await _managmentService.InitializeStudia(requestCookie);

            return Ok(response.Description);
        }

        return Unauthorized();
    }*/
    
    [HttpGet]
    [Route("LoadStudia")]
    public async Task<IActionResult> LoadStudia()
    {
        if (Request.Cookies["token"] != null)
        {
            string requestCookie = Request.Cookies["token"];
            var response = await _managmentService.GetById(requestCookie);
            return Ok(response.Data);
        }
    
        return Unauthorized();
    }
    
    [HttpPost]
    [Route("CreateStudia")]
    public async Task<IActionResult> CreateStudia([FromBody] StudiaViewModel studiaViewModel)
    {
        if (Request.Cookies["token"] != null)
        {
            string requestCookie = Request.Cookies["token"];
            var response = await _managmentService.CreateStudia(studiaViewModel, requestCookie);
           
            return Ok(response.Data);
        }
    
        return Unauthorized();
    }
    
    [HttpPost]
    [Route("CreateAssortment/{studiaId:int}")]
    public async Task<IActionResult> CreateAssortment([FromBody] AssortmentViewModel assortmentViewModel, [FromRoute] int studiaId)
    {
        if (Request.Cookies["token"] != null)
        {
            assortmentViewModel.StudiaId = studiaId;
            var response = await _assortmentService.CreateAssortment(assortmentViewModel);
    
            return Ok(response.Description);
        }
    
        return Unauthorized();
    }


    #endregion
    
    [Route("StudiaPage")]
    public IActionResult StudiaPage()
    {
        return View();
    }
    
    [Route("StudiaManagement")]
    public IActionResult StudiaManagement()
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
    [Route("Register")]
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