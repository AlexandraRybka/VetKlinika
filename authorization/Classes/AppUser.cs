using Microsoft.AspNet.Identity.EntityFramework;

namespace authorization.Classes
{
    public class AppUser : IdentityUser
    {
        public string Country { get; set; }

        public string Age { get; set; }
    }
}