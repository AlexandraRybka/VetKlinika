using authorization.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace authorization.Controllers
{
    public class PetController : Controller
    {
        public readonly IPetsRepository petsRepository;

        public PetController(IPetsRepository petsRepository)
        {
            this.petsRepository = petsRepository;
        }

        [HttpGet]
        public ActionResult Pet()
        {
            IEnumerable<Models.Pet> pets = this.petsRepository.GetPets(User.Identity.Name);
            return View(pets);
        }
             
        [HttpGet]
        public ActionResult AddEditPet(int? id)
        {
            if (id != null)
            {
                Models.Pet pet = this.petsRepository.GetPet(id.Value);
                return View(pet);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddEditPet(Models.Pet model)
        {
            this.petsRepository.AddUpdatePet(model, User.Identity.Name);
            return RedirectToAction("Pet");
        }

        [HttpGet]
        public ActionResult DeletePet(int id)
        {
            this.petsRepository.DeletePet(id);
            return RedirectToAction("Pet");
        }
    }
}