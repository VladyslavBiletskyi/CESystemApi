using System;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;
using CESystemServices.Dto;
using CESystemServicesExtensibility.Dto;
using CESystemServicesExtensibility.Services;

namespace CESystemServices.Services
{
    public class CertificateService : ICertificateService
    {
        private ICertificateRepository _certificateRepository;

        public CertificateService(ICertificateRepository certificateRepository)
        {
            this._certificateRepository = certificateRepository;
        }

        public ICertificateDto GetCertificateById(int id)
        {
            return ToClientDto(_certificateRepository.GetInstanceById(id));
        }

        public bool UpdateCertificate(ICertificateDto certificate)
        {
            var serverCertificate = _certificateRepository.GetInstanceById(certificate.Id);
            serverCertificate.AreaOfUsage = certificate.AreaOfUsage;
            serverCertificate.ExpirationDate = certificate.ExpirationDate;
            serverCertificate.HashingAlgorithm = certificate.HashingAlgorithm;
            serverCertificate.HolderName = certificate.HolderName;
            serverCertificate.IssuerName = certificate.IssuerName;
            serverCertificate.SharedKey = certificate.SharedKey;
            serverCertificate.Signature = certificate.Signature;
            return _certificateRepository.UpdateInstance(serverCertificate);
        }

        public bool RemoveCertificateById(int id)
        {
            var certificate = _certificateRepository.GetInstanceById(id);
            if (certificate == null)
            {
                return true;
            }

            return _certificateRepository.RemoveInstance(certificate);
        }

        private CertificateDto ToClientDto(ICertificate certificate)
        {
            return new CertificateDto
            {
                AreaOfUsage = certificate.AreaOfUsage,
                ExpirationDate = certificate.ExpirationDate,
                HashingAlgorithm = certificate.HashingAlgorithm,
                HolderName = certificate.HolderName,
                IssuerName = certificate.IssuerName,
                SharedKey = certificate.SharedKey,
                Signature = certificate.Signature
            };
        }
    }
}