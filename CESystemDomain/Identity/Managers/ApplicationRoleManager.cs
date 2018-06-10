using CESystemDomain.Entities;
using Microsoft.AspNet.Identity;

namespace CESystemDomain.Identity.Managers
{
    class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {
        }
    }
}