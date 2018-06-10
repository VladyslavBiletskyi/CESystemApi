using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CESystemDomainExtensibility.Entities
{
    public interface ICertificate: IBaseInstance<int>
    {
        IUser User { get; }

        byte[] Hash { get; }

        string HolderName { get; set; }

        string IssuerName { get; set; }

        DateTime ExpirationDate { get; set; }

        byte[] SharedKey { get; set; }

        byte[] Signature { get; set; }

        string AreaOfUsage { get; set; }

        string HashingAlgorithm { get; set; }

        bool IsCertificateValid();
    }
}
