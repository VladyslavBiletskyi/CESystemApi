using CESystemDomainExtensibility.Entities;

namespace CESystemDomainExtensibility.Repositories
{
    public interface ICertificatesHashRepository : ICrudBaseRepository<ICertificatesHash, int>
    {
        void AddCertificateHash(ICertificate certificate);
    }
}
