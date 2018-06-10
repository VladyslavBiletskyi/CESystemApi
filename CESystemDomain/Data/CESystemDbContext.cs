using System.Data.Entity;
using System.Reflection;
using CESystemDomain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CESystemDomain.Data
{
    public class CESystemDbContext : IdentityDbContext<User>
    {
        public CESystemDbContext() : base("CESystemDb", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(CESystemDbContext)));
        }
    }
}
