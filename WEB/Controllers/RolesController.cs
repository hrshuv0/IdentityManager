using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers;

public class RolesController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // GET
    public IActionResult Index()
    {
        var roles = _dbContext.Roles.ToList();
        
        return View(roles);
    }


    [HttpGet]
    public IActionResult Upsert(string id)
    {
        if (string.IsNullOrEmpty(id)) return View();
        
        var roleFromDb = _dbContext.Roles.FirstOrDefault(u => u.Id == id);
        
        return View(roleFromDb);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(IdentityRole role)
    {
        
        if(await _roleManager.RoleExistsAsync(role.Name!))
        {
            return BadRequest("Role already exists");
        }
        
        role.NormalizedName = role.Name!.ToUpper();
        if (string.IsNullOrEmpty(role.Id))
        {
            role.Id = Guid.NewGuid().ToString();
            await _roleManager.CreateAsync(role);
            
            return RedirectToAction(nameof(Index));
        }
        else if (ModelState.IsValid)
        {
            var roleFromDb = _dbContext.Roles.FirstOrDefault(u => u.Id == role.Id);
            if (roleFromDb is null) return NotFound();
                
            roleFromDb.Name = role.Name;
            var result = await _roleManager.UpdateAsync(roleFromDb);
            return RedirectToAction(nameof(Index));
        }
        
        return View(role);
    }


    public IActionResult Delete()
    {
        throw new NotImplementedException();
    }
}