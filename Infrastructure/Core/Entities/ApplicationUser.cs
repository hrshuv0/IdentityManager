using Infrastructure.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Core.Entities;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public Status Status { get; set; }
}