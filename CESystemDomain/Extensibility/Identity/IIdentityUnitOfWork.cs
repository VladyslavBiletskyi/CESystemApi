using System.Threading.Tasks;
using CESystemDomain.Entities;
using CESystemDomainExtensibility.Entities;

namespace CESystemDomain.Extensibility.Identity
{
    public interface IIdentityUnitOfWork
    {
        Task<User> CreateUserAsync(string name, string email, string password);
        Task<User> AuthenticateAsync(string email, string password);
        User TryGetUserById(string userId);
    }
}