using CESystemDomain.Data;
using CESystemDomain.Extensibility.Identity;
using CESystemDomain.Identity;
using CESystemDomain.Repositories;
using CESystemDomainExtensibility.Repositories;
using Ninject.Modules;

namespace CESystemDomain
{
    class DomainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<CESystemDbContext>().ToSelf().InSingletonScope();
            Bind<IIdentityUnitOfWork>().To<IdentityUnitOfWork>();
            Bind(typeof(ICrudBaseRepository<,>)).To(typeof(CrudBaseRepository<,>));
            Bind<IUserRepository>().To<UserRepository>();
            Bind<ICertificatesHashRepository>().To<CertificatesHashRepository>();
            Bind<ICertificateRepository>().To<CertificateRepository>();
        }
    }
}
