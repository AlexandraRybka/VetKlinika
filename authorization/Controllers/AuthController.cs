using authorization.Classes;
using authorization.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace authorization.Controllers
{
    [AllowAnonymous]
    public  class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        private ApplicationRoleManager roleManager;

        public AuthController()
     : this(Startup.UserManagerFactory.Invoke())
        {
        }

        public AuthController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public ApplicationRoleManager RoleManager {
            get {
                return this.roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private  set {
                this.roleManager = value;
            }
        }

        // GET: Auth
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                        
            var user = await userManager.FindAsync(model.Name, model.Password);

            // Don't do this in production!

            if (user != null)
            {
                await SignIn(user);
                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new AppUser
            {
                UserName = model.Name,
                Email = model.Email,
                Country = model.Country,
                Age = model.Age
            };

            var role = this.RoleManager.FindByName("User");
            if (role == null)
            {
                role = new IdentityRole("User");
                this.RoleManager.Create(role);
            }

            var result = await userManager.CreateAsync(user, model.Password);

            var roleForUser = userManager.GetRoles(user.Id);
            if (!roleForUser.Contains(role.Name))
            {
                userManager.AddToRole(user.Id,role.Name);
            }

            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            return Request.GetOwinContext().Authentication;
        }

        private async Task SignIn(AppUser user)
        {
            var identity = await userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);

            identity.AddClaim(new Claim(ClaimTypes.Country, user.Country));

            GetAuthenticationManager().SignIn(identity);
        }
    }
}