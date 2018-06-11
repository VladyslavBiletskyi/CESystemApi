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

        public async Task<UserDto> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.AuthenticateAsync(email, password);
            return new UserDto{Email = user.Email, Id = user.Id, Name = user.Name};
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