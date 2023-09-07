using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers;

public class AccessCheckerController : Controller
{
    // GET
    public IActionResult AllAccess()
    {
        return View();
    }
    
    public IActionResult AuthorizedAccess()
    {
        return View();
    }
    
    public IActionResult UserAccess()
    {
        return View();
    }
    
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