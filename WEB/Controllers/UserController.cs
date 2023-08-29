using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers;

public class UserController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(ApplicationDbContext db, 
        UserManager<IdentityUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    // GET
    public IActionResult Index()
    {
        var userList = _db.ApplicationUsers!.ToList();
        var userRole = _db.UserRoles.ToList();
        
        var roles = _db.Roles.ToList();
        
        foreach (var user in userList)
        {
            var role = userRole.FirstOrDefault(u => u.UserId == user.Id);
            if (role is null)
            {
                user.Role = "None";
            }
            else
            {
                user.Role = roles.FirstOrDefault(u => u.Id == role.RoleId)!.Name!;
            }
        }
        
        return View(userList);
    }
}