using System;
using System.Collections.Generic;
using System.Linq;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;
using CESystemServices.Extensibility.Generators;
using CESystemServicesExtensibility.Dto;
using CESystemServicesExtensibility.Services;

namespace CESystemServices.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IUserRepository userRepository;
        private readonly ICertificateGenerator certificateGenerator;

        public CertificateService(ICertificateRepository certificateRepository, ICertificateGenerator certificateGenerator, IUserRepository userRepository)
        {
            this._certificateRepository = certificateRepository;
            this.certificateGenerator = certificateGenerator;
            this.userRepository = userRepository;
        }

        public CertificateDto GetCertificateById(int id)
        {
            return ToClientDto(_certificateRepository.GetInstanceById(id));
        }

        public IEnumerable<CertificateDto> GetAllCertificates(string userId)
        {
            var systemUser = userRepository.GetInstanceById(userId);
            if (systemUser != null)
            {
                return systemUser.Certificates.Select(ToClientDto);
            }

            return Enumerable.Empty<CertificateDto>();
        }

        public bool UpdateCertificate(CertificateDto certificate)
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

        public bool AddCertificate(UserDto user, CertificateDto certificate)
        {
            var systemUser = userRepository.GetInstanceById(user.Id);
            var createdCertificate = _certificateRepository.CreateCertificate(
                systemUser,
                certificate.HolderName,
                certificate.IssuerName,
                certificate.ExpirationDate,
                certificate.SharedKey,
                certificate.Signature,
                certificate.AreaOfUsage,
                certificate.HashingAlgorithm);
            if (createdCertificate != null)
            {
                return true;
            }

            return false;
        }

        public bool IsCertificateBelongsToUser(string userId, int certificateId)
        {
            var user = userRepository.GetInstanceById(userId);
            var certificate = _certificateRepository.GetInstanceById(certificateId);
            return certificate != null && user != null && certificate.User.Id == user.Id;
        }

        public CertificateDto GenerateChildCertificate(CertificateDto certificate, string areaOfUsage, string holderName, DateTime expirationDate)
        {
            var parent = _certificateRepository.GetInstanceById(certificate.Id);
            return parent == null ? null : ToClientDto(certificateGenerator.GenerateChildCertificate(parent, areaOfUsage, holderName, expirationDate));
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
                Signature = certificate.Signature,
                Children = certificate.Children.Select(ToClientDto)
            };
        }
    }
}