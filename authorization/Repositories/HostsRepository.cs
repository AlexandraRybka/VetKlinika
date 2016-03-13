using authorization.Models;
using authorization.Repositories.Interfaces;
using System.Linq;

namespace authorization.Repositories
{
    public class HostsRepository: IHostsRepository
    {
        public Host GetHost(string userName)
        {
            Host host = new Host();
            using (DbEntities db = new DbEntities())
            {
                string userId = db.AspNetUsers.First(user => user.UserName.Equals(userName)).Id;
                if (db.Hosts.FirstOrDefault(user => user.Id_Login.Equals(userId)) != null)
                {
                    host.Name = db.Hosts.First(user => user.Id_Login.Equals(userId)).Name;
                    host.Surname = db.Hosts.First(user => user.Id_Login.Equals(userId)).Surname;
                    host.Location = db.Hosts.First(user => user.Id_Login.Equals(userId)).Location;
                    host.Phone = db.Hosts.First(user => user.Id_Login.Equals(userId)).Phone;
                }
            }
            return host;
        }

        public void UpdateHost(Host host, string userName)
        {
            using (DbEntities db = new DbEntities())
            {
                string userId = db.AspNetUsers.First(user => user.UserName.Equals(userName)).Id;
                var item = db.Hosts.FirstOrDefault(user => user.Id_Login.Equals(userId));
                if (item != null)
                {
                    item.Name = host.Name;
                    item.Surname = host.Surname;
                    item.Location = host.Location;
                    item.Phone = host.Phone;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    db.Hosts.Add(new Hosts
                    {
                        Id_Login = userId,
                        Name = host.Name,
                        Surname = host.Surname,
                        Location = host.Location,
                        Phone = host.Phone
                    });
                }
                db.SaveChanges();
            }
        }
    }
}