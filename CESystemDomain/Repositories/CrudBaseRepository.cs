using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CESystemDomain.Data;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;

namespace CESystemDomain.Repositories
{
    internal abstract class CrudBaseRepository<TInterface, TIdType> : ICrudBaseRepository<TInterface, TIdType>  where TInterface : IBaseInstance<TIdType>
    {
        protected CESystemDbContext DbContext;

        protected CrudBaseRepository(CESystemDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual TInterface GetInstanceById(TIdType id)
        {
            return GetQueryable().FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual IEnumerable<TInterface> GetAllInstances()
        {
            return GetQueryable();
        }

        public abstract bool UpdateInstance(TInterface instance);

        public abstract bool RemoveInstance(TInterface instance);

        protected abstract IQueryable<TInterface> GetInitialQueryable();

        protected IQueryable<TInterface> GetQueryable()
        {
            var queryable = GetInitialQueryable();
            foreach (var property in typeof(TInterface).GetProperties())
            {
                if (property.GetGetMethod().ReturnType.BaseType == typeof(IBaseInstance<>) || property.GetGetMethod().IsVirtual)
                {
                    queryable = queryable.Include(property.Name);
                }
            }

            return queryable;
        }
    }
}
