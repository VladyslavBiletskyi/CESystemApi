using CESystemDomain.Entities;
using Microsoft.AspNet.Identity;

namespace CESystemDomain.Identity.Managers
{
    class ApplicationUserManager: UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store) : base(store)
        {
        }
    }
}