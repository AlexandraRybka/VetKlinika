using Microsoft.AspNet.Identity.EntityFramework;

namespace authorization.Classes
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("DefaultConnection")
        {
        }
    }
}