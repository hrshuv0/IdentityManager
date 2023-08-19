using Infrastructure.Core.Entities.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers;

public class AccountController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        var model = new RegisterVm();
        
        return View(model);
    }
}