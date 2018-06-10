using System;
using System.Linq;
using CESystemDomain.Data;
using CESystemDomain.Entities;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;

namespace CESystemDomain.Repositories
{
    internal class CertificateRepository : CrudBaseRepository<ICertificate, int>, ICertificateRepository
    {
        public CertificateRepository(CESystemDbContext dbContext) : base(dbContext)
        {
        }

        public override bool UpdateInstance(ICertificate instance)
        {
            var certificate = DbContext.Set<Certificate>().Find(instance.Id);
            if (certificate == null)
            {
                return false;
            }

            certificate.ExpirationDate = instance.ExpirationDate;
            certificate.HolderName = instance.HolderName;
            certificate.IssuerName = instance.IssuerName;
            certificate.SharedKey = instance.SharedKey;
            certificate.Signature = instance.Signature;
            if (certificate.IsCertificateValid())
            {
                DbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public override bool RemoveInstance(ICertificate instance)
        {
            try
            {
                var certificate = DbContext.Set<Certificate>().Find(instance.Id);
                if (certificate == null)
                {
                    return false;
                }

                DbContext.Set<Certificate>().Remove(certificate);
                DbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public ICertificate CreateCertificate(IUser user, string holderName, string issuerName, DateTime expirationDate, byte[] sharedKey, byte[] signature, string areaOfUsage, string hashingAlgorithm, ICertificate parent = null)
        {
            var owner = DbContext.Set<User>().Find(user.Id);
            if (owner == null)
            {
                return null;
            }

            var certificate = new Certificate
            {
                ExpirationDate = expirationDate,
                HolderName = holderName,
                IssuerName = issuerName,
                SharedKey = sharedKey,
                Signature = signature,
                Owner = owner,
                AreaOfUsage = areaOfUsage,
                HashingAlgorithm = hashingAlgorithm,
                ParentCertificate = parent
            };
            if (certificate.IsCertificateValid())
            {
                DbContext.Set<Certificate>().Add(certificate);
                DbContext.SaveChanges();
                return certificate;
            }

            return null;
        }

        protected override IQueryable<ICertificate> GetInitialQueryable()
        {
            return DbContext.Set<Certificate>().AsQueryable();
        }
    }
}
