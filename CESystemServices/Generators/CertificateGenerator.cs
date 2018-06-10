using System;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;
using CESystemServices.Extensibility.Generators;

namespace CESystemServices.Generators
{
    internal class CertificateGenerator : ICertificateGenerator
    {
        private ICertificateRepository certificateRepository;

        public CertificateGenerator(ICertificateRepository certificateRepository)
        {
            this.certificateRepository = certificateRepository;
        }

        public ICertificate GenerateChildCertificate(ICertificate parent, string areaOfUsage, string holderName, DateTime expirationDate)
        {
            var child = GenerateChildInternal(parent);
            if (StoreGeneratedCertificate(child))
            {
                return child;
            }

            return null;
        }

        private ICertificate GenerateChildInternal(ICertificate parent)
        {
            return null;
        }

        private bool StoreGeneratedCertificate(ICertificate certificate)
        {
            return certificateRepository.CreateCertificate(certificate.User, certificate.HolderName,
                certificate.IssuerName, certificate.ExpirationDate, certificate.SharedKey, certificate.Signature,
                certificate.AreaOfUsage, certificate.HashingAlgorithm, certificate.ParentCertificate) == null;
        }
    }
}