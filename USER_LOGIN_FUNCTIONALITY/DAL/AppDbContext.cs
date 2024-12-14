using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using USER_LOGIN_FUNCTIONALITY.Models;

namespace USER_LOGIN_FUNCTIONALITY.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

    }
}
