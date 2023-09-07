using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers;

[Authorize]
public class AccessCheckerController : Controller
{
    [AllowAnonymous]
    public IActionResult AllAccess()
    {
        return View();
    }
    
    [Authorize]
    public IActionResult AuthorizedAccess()
    {
        return View();
    }
    
    [Authorize(Roles = "User")]
    public IActionResult UserAccess()
    {
        return View();
    }
    
    [Authorize(Roles = "User,Admin")]
    public IActionResult UserOrAdminAccess()
    {
        return View();
    }
    
    [Authorize(Policy = "Admin")]
    public IActionResult AdminAccess()
    {
        return View();
    }
    
    public IActionResult Admin_CreateAccess()
    {
        return View();
    }
    
    public IActionResult Admin_Create_Edit_Delete()
    {
        return View();
    }
    
    public IActionResult Admin_Create_Edit_Delete_SuperAdmin()
    {
        return View();
    }
    
    
    
}