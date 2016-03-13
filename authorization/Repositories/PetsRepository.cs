using authorization.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace authorization.Repositories
{
    public class PetsRepository: IPetsRepository
    {

        public IEnumerable<authorization.Models.Pet> GetPets(string userName)
        {
            IList<Models.Pet> pets = new List<Models.Pet>();
            using (DbEntities db = new DbEntities())
            {
                string userId = db.AspNetUsers.First(user => user.UserName.Equals(userName)).Id;
                var firstPet = db.Pets.FirstOrDefault(pet => pet.Id_Host.Equals(userId));
                if (firstPet != null)
                {
                    var collectionOfPet = db.Pets.Where(pet => pet.Id_Host.Equals(userId));
                    foreach (var item in collectionOfPet)
                    {
                        Models.Pet pet = new Models.Pet();
                        pet.Type = item.Type;
                        pet.Name = item.Name;
                        pet.Age = item.Age;
                        pet.Diagnosis = item.Diagnosis;
                        pet.Id = item.Id;
                        pets.Add(pet);
                    }
                }
            }
            return pets;
        }

        public void AddUpdatePet(Models.Pet pet, string userName)
        {
            using (DbEntities db = new DbEntities())
            {
                var item = db.Pets.FirstOrDefault(user => user.Id.Equals(pet.Id));
                string userId = db.AspNetUsers.First(user => user.UserName.Equals(userName)).Id;
                /*update pet*/
                if (item != null)
                {
                    item.Type = pet.Type;
                    item.Name = pet.Name;
                    item.Age = pet.Age;
                    item.Diagnosis = pet.Diagnosis;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                else/*add pet*/
                {
                    db.Pets.Add(new Pets
                    {
                        Type = pet.Type,
                        Name = pet.Name,
                        Age = pet.Age,
                        Diagnosis = pet.Diagnosis,
                        Id_Host = userId
                    });
                }
                db.SaveChanges();
            }
        }

        public Models.Pet GetPet(int id)
        {
            Models.Pet pet = new Models.Pet();
            using (DbEntities db = new DbEntities())
            {
                var item = db.Pets.FirstOrDefault(model => model.Id.Equals(id));
                if (item != null)
                {
                    pet.Type = item.Type;
                    pet.Name = item.Name;
                    pet.Age = item.Age;
                    pet.Diagnosis = item.Diagnosis;
                }
            }
            return pet;
        }

        public void DeletePet(int id)
        {
            using (DbEntities db = new DbEntities())
            {
                var pet = db.Pets.First(item => item.Id.Equals(id));
                db.Entry(pet).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}