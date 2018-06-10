using System.Linq;
using System.Threading.Tasks;
using CESystemDomain.Data;
using CESystemDomain.Entities;
using CESystemDomain.Extensibility.Identity;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;

namespace CESystemDomain.Repositories
{
    internal class UserRepository : CrudBaseRepository<IUser, string>, IUserRepository
    {
        private IIdentityUnitOfWork identityUnitOfWork;

        public UserRepository(CESystemDbContext dbContext, IIdentityUnitOfWork identityUnitOfWork) : base(dbContext)
        {
            this.identityUnitOfWork = identityUnitOfWork;
        }

        public override IUser GetInstanceById(string id)
        {
            return identityUnitOfWork.TryGetUserById(id);
        }

        public override bool UpdateInstance(IUser instance)
        {
            var user = identityUnitOfWork.TryGetUserById(instance.Id);
            if (user == null) return false;
            user.Email = instance.Email;
            user.Name = instance.Name;
            DbContext.SaveChanges();
            return true;
        }

        public override bool RemoveInstance(IUser instance)
        {
            try
            {
                var user = identityUnitOfWork.TryGetUserById(instance.Id);
                if (user == null) return false;
                DbContext.Set<User>().Remove(user);
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IUser> CreateUserAsync(string name, string email, string password)
        {
            return await identityUnitOfWork.CreateUserAsync(name, email, password);
        }

        public async Task<IUser> AuthenticateAsync(string email, string password)
        {
            return await identityUnitOfWork.AuthenticateAsync(email, password);
        }

        protected override IQueryable<IUser> GetInitialQueryable()
        {
            return DbContext.Set<User>().AsQueryable();
        }
    }
}
