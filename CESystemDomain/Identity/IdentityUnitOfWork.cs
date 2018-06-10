using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using CESystemDomain.Data;
using CESystemDomain.Entities;
using CESystemDomain.Extensibility.Identity;
using CESystemDomain.Identity.Managers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CESystemDomain.Identity
{
    internal class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private readonly CESystemDbContext dbContext;
        private readonly ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public IdentityUnitOfWork(CESystemDbContext dbContext)
        {
            this.dbContext = dbContext;
            userManager = new ApplicationUserManager(new UserStore<User>(dbContext));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(dbContext));
        }

        public async Task<User> CreateUserAsync(string name, string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User{Email = email, UserName = name };
                var result = await userManager.CreateAsync(user, password);
                if (!result.Errors.Any())
                {
                    await userManager.AddToRoleAsync(user.Id, "SystemUser");
                    await dbContext.SaveChangesAsync();
                    return user;
                }
                else
                {
                    throw new DbEntityValidationException(result.Errors.First());
                }
            }

            return null;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await userManager.FindAsync(email, password);
            if (user != null)
            {
                await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);
                return user;
            }
            return null;
        }

        public User TryGetUserById(string userId)
        {
            return userManager.FindById(userId);
        }
    }
}