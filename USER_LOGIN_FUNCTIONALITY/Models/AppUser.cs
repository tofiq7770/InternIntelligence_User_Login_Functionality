using Microsoft.AspNetCore.Identity;

namespace USER_LOGIN_FUNCTIONALITY.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
