using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace authorization.Classes
{
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        private readonly UserManager<AppUser> userManager;

        public AppViewPage()
     : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public AppViewPage(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        protected AppUser CurrentUser
        {
            get
            {
                return userManager.FindByName(User.Identity.Name);
            }
        }
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
}