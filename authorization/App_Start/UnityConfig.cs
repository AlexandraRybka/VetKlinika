using authorization.Classes;
using authorization.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Mvc;
using Unity.Mvc5;

namespace authorization
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.LoadConfiguration();

            container.RegisterType<AuthController>(new InjectionConstructor());
            container.RegisterType<IUserStore<AppUser>, UserStore<AppUser>>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            
        }
    }
}