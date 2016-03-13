using authorization.Models;
using authorization.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace authorization.Repositories
{
    public class DoctorsRepository: IDoctorsRepository
    {
        public Doctor GetDoctor(int id)
        {
            Doctor doctor = new Doctor();
            using (DbEntities db = new DbEntities())
            {
                var identityDoctor = db.Doctors.FirstOrDefault(user => user.Id.Equals(id));
                if (identityDoctor != null)
                {
                    doctor.Id = identityDoctor.Id;
                    doctor.FirstName = identityDoctor.FirstName;
                    doctor.LastName = identityDoctor.LastName;
                    doctor.MiddleName = identityDoctor.MiddleName;
                    doctor.Address = identityDoctor.Address;
                    doctor.Age = identityDoctor.Age;
                    doctor.Category = identityDoctor.Category;
                    doctor.Position = identityDoctor.Position;
                    doctor.Qualification = identityDoctor.Qualification;
                }
            }
            return doctor;
        }

        public IEnumerable<Models.Doctor> GetDoctors()
        {
            IList<Models.Doctor> doctors = new List<Models.Doctor>();
            using (DbEntities db = new DbEntities())
            {
                foreach (var item in db.Doctors)
                {
                    doctors.Add(new Doctor
                    {
                        Id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        MiddleName = item.MiddleName,
                        Address = item.Address,
                        Phone = item.Phone,
                        Age = item.Age,
                        Category = item.Category,
                        Position = item.Position,
                        Qualification = item.Qualification,
                    });
                }
            }
            return doctors;
        }

        public void UpdateDoctor(Doctor doctor)
        {
            using (DbEntities db = new DbEntities())
            {
                var item = db.Doctors.FirstOrDefault(user => user.Id.Equals(doctor.Id));
                if (item != null)
                {
                    item.FirstName = doctor.FirstName;
                    item.LastName = doctor.LastName;
                    item.MiddleName = doctor.MiddleName;
                    item.Address = doctor.Address;
                    item.Age = doctor.Age;
                    item.Category = doctor.Category;
                    item.Position = doctor.Position;
                    item.Qualification = doctor.Qualification;
                    item.Phone = doctor.Phone;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Doctors.Add(new Doctors
                    {
                        Id = doctor.Id,
                        FirstName = doctor.FirstName,
                        LastName = doctor.LastName,
                        MiddleName = doctor.MiddleName,
                        Address = doctor.Address,
                        Phone = doctor.Phone,
                        Age = doctor.Age,
                        Category = doctor.Category,
                        Position = doctor.Position,
                        Qualification = doctor.Qualification,
                    });
                }
                db.SaveChanges();
            }
        }

        public void DeletedDoctor(int id)
        {
            using (DbEntities db = new DbEntities())
            {
                var doctor = db.Doctors.First(item => item.Id.Equals(id));
                db.Entry(doctor).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}