using System.Threading.Tasks;
using CESystemDomainExtensibility.Entities;

namespace CESystemDomainExtensibility.Repositories
{
    public interface IUserRepository: ICrudBaseRepository<IUser, string>
    {
        Task<IUser> CreateUserAsync(string name, string email, string password);
        Task<IUser> AuthenticateAsync(string email, string password);
    }
}
