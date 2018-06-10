using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CESystemDomainExtensibility.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IUser = CESystemDomainExtensibility.Entities.IUser;

namespace CESystemDomain.Entities
{
    public class User : IdentityUser, IUser
    {
        public string Name { get; set; }

        public IEnumerable<ICertificate> Certificates => OwnedCertificates.ToList();

        public virtual ICollection<Certificate> OwnedCertificates { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Здесь добавьте настраиваемые утверждения пользователя
            return userIdentity;
        }
    }
}
