using System.Linq;
using System.Security.Claims;
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
        private ICertificateRepository certificateRepository;

        public UserRepository(CESystemDbContext dbContext, IIdentityUnitOfWork identityUnitOfWork, ICertificateRepository certificateRepository) : base(dbContext)
        {
            this.identityUnitOfWork = identityUnitOfWork;
            this.certificateRepository = certificateRepository;
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
                foreach (var certificate in user.Certificates)
                {
                    certificateRepository.RemoveInstance(certificate);
                }
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

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(string userId, string authenticationType)
        {
            throw new System.NotImplementedException();
        }

        protected override IQueryable<IUser> GetInitialQueryable()
        {
            return DbContext.Set<User>().AsQueryable();
        }
    }
}
