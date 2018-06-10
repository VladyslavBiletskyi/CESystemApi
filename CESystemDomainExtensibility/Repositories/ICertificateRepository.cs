using System;
using CESystemDomainExtensibility.Entities;

namespace CESystemDomainExtensibility.Repositories
{
    public interface ICertificateRepository : ICrudBaseRepository<ICertificate, int>
    {
        ICertificate CreateCertificate(IUser user, string holderName, string issuerName,
            DateTime expirationDate, byte[] sharedKey, byte[] signature, string areaOfUsage, string hashingAlgorithm, ICertificate parent = null);
    }
}
