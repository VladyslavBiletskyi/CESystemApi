using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CESystemServicesExtensibility.Dto;

namespace CESystemServicesExtensibility.Services
{
    public interface IUserService : IDisposable
    {
        Task<UserDto> AuthenticateAsync(string email, string password);

        Task<ClaimsIdentity> GenerateUserIdentityAsync(string userId, string authenticationType);
    }
}
