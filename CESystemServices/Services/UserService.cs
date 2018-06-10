using System.Security.Claims;
using System.Threading.Tasks;
using CESystemDomainExtensibility.Repositories;
using CESystemServicesExtensibility.Dto;
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

        public Task<UserDto> AuthenticateAsync(string email, string password)
        {
            var user = _userRepository.AuthenticateAsync(email, password);
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(string userId, string authenticationType)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}