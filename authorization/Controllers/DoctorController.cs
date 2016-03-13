using authorization.Models;
using authorization.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace authorization.Controllers
{
    public class DoctorController : Controller
    {
        public readonly IDoctorsRepository doctorsRepository;

        public DoctorController(IDoctorsRepository doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        [HttpGet]
        public ActionResult Doctor()
        {
            IEnumerable<Models.Doctor> doctors = this.doctorsRepository.GetDoctors();
            return View(doctors);
        }

        [HttpGet]
        public ActionResult AddEditDoctor(int? id)
        {
            if (id != null)
            {
                Doctor doctor = this.doctorsRepository.GetDoctor(id.Value);
                return View(doctor);
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddEditDoctor(Doctor model)
        {
            this.doctorsRepository.UpdateDoctor(model);
            return RedirectToAction("Doctor");
        }

        [HttpGet]
        public ActionResult DeleteDoctor(int id)
        {
            this.doctorsRepository.DeletedDoctor(id);
            return RedirectToAction("Doctor");
        }
    }
}