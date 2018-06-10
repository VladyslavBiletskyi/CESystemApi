using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESystemDomainExtensibility.Entities
{
    public interface IUser : IBaseInstance<string>
    {
        string Name { get; set; }

        string Email { get; set; }

        IEnumerable<ICertificate> Certificates { get; }
    }
}
