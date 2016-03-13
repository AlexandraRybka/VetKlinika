using authorization.Models;

namespace authorization.Repositories.Interfaces
{
    public interface IHostsRepository
    {
        Host GetHost(string userName);
        void UpdateHost(Host host, string userName);
    }
}
