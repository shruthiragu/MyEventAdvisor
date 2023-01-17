using System.Security.Claims;
using System.Security.Principal;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class IdentityService : IIdentityService<ApplicationUser>
    {
        public ApplicationUser Get(IPrincipal principal)
        {
            if (principal is ClaimsPrincipal claimsPrincipal)
            {
                return new ApplicationUser
                {
                    Id = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == "preferred_username")?.Value ?? "",
                    Email = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == "preferred_username")?.Value ?? ""
                };
            }
            else throw new ArgumentException();
        }
    }
}
