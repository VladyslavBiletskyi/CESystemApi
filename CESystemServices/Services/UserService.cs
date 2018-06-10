using CESystemDomainExtensibility.Repositories;
using CESystemServicesExtensibility.Services;

namespace CESystemServices.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }
    }
}