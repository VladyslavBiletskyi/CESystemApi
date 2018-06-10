using CESystemServices.Extensibility.Generators;
using CESystemServices.Generators;
using CESystemServices.Services;
using CESystemServicesExtensibility.Services;
using Ninject.Modules;

namespace CESystemServices
{
    public class ServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICertificateGenerator>().To<CertificateGenerator>();
            Bind<ICertificateService>().To<CertificateService>();
            Bind<IUserService>().To<UserService>();
            Bind<IDbHealthKeeperService>().To<DbHealthKeeperService>();
        }
    }
}