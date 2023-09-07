namespace Infrastructure.Core.Entities.ViewModel;

public class UserClaimsVm
{
    public UserClaimsVm()
    {
        ClaimList = new List<UserClaim>();
    }
    
    public string UserId { get; set; }
    public List<UserClaim> ClaimList { get; set; }
}

public class UserClaim
{
    public string ClaimTypes { get; set; }
    public bool IsSelected { get; set; }
}