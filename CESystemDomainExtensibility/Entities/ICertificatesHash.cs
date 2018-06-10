using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESystemDomainExtensibility.Entities
{
    public interface ICertificatesHash: IBaseInstance<int>
    {
        ICertificate Certificate { get; }
        byte[] Hash { get; set; }
    }
}
