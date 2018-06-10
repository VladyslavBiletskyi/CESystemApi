using System.Linq;
using CESystemDomain.Data;
using CESystemDomain.Entities;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;

namespace CESystemDomain.Repositories
{
    internal class CertificatesHashRepository : CrudBaseRepository<ICertificatesHash, int>, ICertificatesHashRepository
    {
        public CertificatesHashRepository(CESystemDbContext dbContext) : base(dbContext)
        {
        }

        public override bool UpdateInstance(ICertificatesHash instance)
        {
            try
            {
                var certificateHash = DbContext.Set<CertificateHash>().Find(instance.Id);
                if (certificateHash == null)
                {
                    return false;
                }

                certificateHash.Hash = instance.Hash;
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool RemoveInstance(ICertificatesHash instance)
        {
            try
            {
                var certificateHash = DbContext.Set<CertificateHash>().Find(instance.Id);
                if (certificateHash == null)
                {
                    return false;
                }

                DbContext.Set<CertificateHash>().Remove(certificateHash);
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddCertificateHash(ICertificate certificate)
        {
            var originalCertificate = DbContext.Set<Certificate>().Find(certificate.Id);
            if (originalCertificate == null)
            {
                return;
            }
            var certificateHash = new CertificateHash { ReferredCertificate = originalCertificate, Hash = originalCertificate.Hash };
            DbContext.Set<CertificateHash>().Add(certificateHash);
            DbContext.SaveChanges();
        }

        protected override IQueryable<ICertificatesHash> GetInitialQueryable()
        {
            return DbContext.Set<CertificateHash>().AsQueryable();
        }
    }
}
