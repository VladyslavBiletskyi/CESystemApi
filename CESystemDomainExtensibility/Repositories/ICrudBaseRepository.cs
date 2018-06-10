using System.Collections.Generic;
using CESystemDomainExtensibility.Entities;

namespace CESystemDomainExtensibility.Repositories
{
    public interface ICrudBaseRepository<TInterface, TIdType> where TInterface : IBaseInstance<TIdType>
    {
        TInterface GetInstanceById(TIdType id);

        IEnumerable<TInterface> GetAllInstances();

        bool UpdateInstance(TInterface instance);

        bool RemoveInstance(TInterface instance);
    }
}