using authorization.Models;
using System.Collections.Generic;

namespace authorization.Repositories.Interfaces
{
    public interface IDoctorsRepository
    {
        Doctor GetDoctor(int id);
        IEnumerable<Models.Doctor> GetDoctors();
        void UpdateDoctor(Doctor doctor);
        void DeletedDoctor(int id);
    }
}
