using System;
using CESystemDomainExtensibility.Entities;

namespace CESystemDomainExtensibility.Repositories
{
    public interface ICertificateRepository : ICrudBaseRepository<ICertificate, int>
    {
        ICertificate CreateCertificate(IUser user, byte[] hash, string holderName, string issuerName,
            DateTime expirationDate, byte[] sharedKey, byte[] signature, string areaOfUsage, string hashingAlgorithm);
    }
}
