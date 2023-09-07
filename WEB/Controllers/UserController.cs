using System.Security.Claims;
using Infrastructure.Core.Entities.ViewModel;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    [HttpGet]
    public async Task<IActionResult> ManageUserClaims(string userId)
    {
        var user = _db.ApplicationUsers!.FirstOrDefault(u => u.Id == userId);
        if(user is null)
        {
            return NotFound();
        }
        
        var existingClaims = await _userManager.GetClaimsAsync(user);


        var model = new UserClaimsVm
        {
            UserId = userId
        };
        
        foreach (var claim in ClaimStore.claimList)
        {
            UserClaim userClaim = new UserClaim()
            {
                ClaimTypes = claim.Type
            };
            if (existingClaims.Any(c => c.Type == claim.Type))
            {
                userClaim.IsSelected = true;
            }
            model.ClaimList.Add(userClaim);
            
        }
        
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ManageUserClaims(UserClaimsVm model)
    {
        var user = await _db.ApplicationUsers!.FirstOrDefaultAsync(u => u.Id == model.UserId);
        if(user is null)
        {
            return NotFound();
        }

        var claims = await _userManager.GetClaimsAsync(user);
        var result = await _userManager.RemoveClaimsAsync(user, claims);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot remove user existing claims");
            return View(model);
        }

        result = await _userManager.AddClaimsAsync(user, model.ClaimList.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimTypes, c.IsSelected.ToString())));
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot add selected claims to user");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
    

    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }

    public IActionResult Edit()
    {
        throw new NotImplementedException();
    }

    public IActionResult LockUnlock()
    {
        throw new NotImplementedException();
    }
}