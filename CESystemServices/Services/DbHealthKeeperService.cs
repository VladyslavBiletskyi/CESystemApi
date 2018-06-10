using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;
using CESystemServicesExtensibility.Services;

namespace CESystemServices.Services
{
    public class DbHealthKeeperService : IDbHealthKeeperService
    {
        private readonly ICertificatesHashRepository certificatesHashRepository;
        private readonly ICertificateRepository certificateRepository;

        public DbHealthKeeperService(ICertificatesHashRepository certificatesHashRepository, ICertificateRepository certificateRepository)
        {
            this.certificatesHashRepository = certificatesHashRepository;
            this.certificateRepository = certificateRepository;
        }

        public void Run()
        {
            var changedCertificates = FindChangedCertificates().ToList();
            foreach (var changedCertificate in changedCertificates)
            {
                if (changedCertificate.IsCertificateValid())
                {
                    var hash = certificatesHashRepository.GetAllInstances()
                        .FirstOrDefault(x => x.Certificate.Id == changedCertificate.Id);
                    if (hash != null)
                    {
                        hash.Hash = changedCertificate.Hash;
                        certificatesHashRepository.UpdateInstance(hash);

                    }
                    else
                    {
                        certificatesHashRepository.AddCertificateHash(changedCertificate);
                    }
                }
                else
                {
                    RestoreCertificateFromDump(changedCertificate);
                }
            }
            CreateDbDump(changedCertificates);
        }

        private IEnumerable<ICertificate> FindChangedCertificates()
        {
            var result = new LinkedList<ICertificate>();
            var certificatesHashes = certificatesHashRepository.GetAllInstances().ToList();
            foreach (var certificate in certificateRepository.GetAllInstances().ToList())
            {
                var certificateHash = certificatesHashes.FirstOrDefault(x => x.Certificate.Id == certificate.Id);
                if (certificateHash == null)
                {
                    result.AddLast(certificate);
                }
                else
                {
                    if (certificateHash.Hash != certificate.Hash)
                    {
                        result.AddLast(certificate);
                    }
                }
            }
            return result;
        }

        private void RestoreCertificateFromDump(ICertificate certificate)
        {
            throw new System.NotImplementedException();
        }

        public void CreateDbDump(IEnumerable<ICertificate> changedCertificates)
        {
            if (changedCertificates.Any())
            {

            }
        }
    }
}