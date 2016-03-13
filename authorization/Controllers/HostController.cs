using authorization.Models;
using authorization.Repositories.Interfaces;
using System.Web.Mvc;

namespace authorization.Controllers
{
    [Authorize(Roles = "User")]
    public class HostController : Controller
    {
        public readonly IHostsRepository hostsRepository;

        public HostController(IHostsRepository hostsRepository)
        {
            this.hostsRepository = hostsRepository;
        }

        [HttpGet]
        public ActionResult Host()
        {
            Host host = this.hostsRepository.GetHost(User.Identity.Name);
            return View(host);
        }

        [HttpGet]
        public ActionResult EditHost()
        {
            Host host = this.hostsRepository.GetHost(User.Identity.Name);
            return View(host);
        }

        [HttpPost]
        public ActionResult EditHost(Host model)
        {
            this.hostsRepository.UpdateHost(model, User.Identity.Name);
            return RedirectToAction("Host");
        }        
        
    }
}