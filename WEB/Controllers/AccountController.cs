using Infrastructure.Core.Entities;
using Infrastructure.Core.Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<ApplicationUser> userManager, 
    SignInManager<ApplicationUser> signInManager, 
    RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

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
    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if(result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(model);
        } 
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Register()
    {
        if(!await _roleManager.RoleExistsAsync("Admin"))
        {
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }
        
        var listItems = new List<SelectListItem>
        {
            new() { Text = "Admin", Value = "Admin" },
            new() { Text = "User", Value = "User" }
        };

        var model = new RegisterVm()
        {
            RoleList = listItems
        };
        
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new ApplicationUser()
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name
        };
        
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(model.RoleSelected) && model.RoleSelected == "Admin")
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            
            await _signInManager.SignInAsync(user, false);
        }
        else
        {
            var listItems = new List<SelectListItem>
            {
                new() { Text = "Admin", Value = "Admin" },
                new() { Text = "User", Value = "User" }
            };

            model.RoleList = listItems;
            
            AddErrors(result);
            return View(model);
        }
        
        return RedirectToAction("Index", "Home");
        
    }
    
    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        
        return RedirectToAction("Index", "Home");
    }
}