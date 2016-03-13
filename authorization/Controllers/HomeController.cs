using authorization.Models;
using authorization.Repositories.Interfaces;
using System.Linq;
using System.Web.Mvc;

namespace authorization.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public readonly IReviewsRepository reviewsRepository;

        public HomeController(IReviewsRepository reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }
        
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            SheetsComments rv = new SheetsComments();
            rv.Reviews = reviewsRepository.GetReviews().ToList();
            return View(rv);
        }

        [HttpPost]
        public ActionResult Index(SheetsComments model)
        {
            reviewsRepository.AddReview(User.Identity.Name, model.PersonalReview);
            model.Reviews = reviewsRepository.GetReviews().ToList();
            model.PersonalReview = string.Empty;
            return View(model);
        }

        public ActionResult Appointments()
        {
            return View();
        }

        public ActionResult Donation()
        {
            return View();
        }
    }
}