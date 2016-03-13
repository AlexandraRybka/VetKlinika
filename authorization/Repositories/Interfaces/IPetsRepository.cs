using System.Collections.Generic;

namespace authorization.Repositories.Interfaces
{
    public interface IPetsRepository
    {
        IEnumerable<authorization.Models.Pet> GetPets(string userName);
        void AddUpdatePet(Models.Pet pet, string userName);
        Models.Pet GetPet(int id);
        void DeletePet(int id);
    }
}
