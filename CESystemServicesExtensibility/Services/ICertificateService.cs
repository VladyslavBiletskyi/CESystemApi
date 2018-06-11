using System;
using System.Collections.Generic;
using CESystemServicesExtensibility.Dto;

namespace CESystemServicesExtensibility.Services
{
    public interface ICertificateService
    {
        CertificateDto GetCertificateById(int id);
        IEnumerable<CertificateDto> GetAllCertificates(string userId);

        bool UpdateCertificate(CertificateDto certificate);

        bool RemoveCertificateById(int id);

        bool AddCertificate(UserDto user, CertificateDto certificate);

        bool IsCertificateBelongsToUser(string userId, int certificateId);

        CertificateDto GenerateChildCertificate(CertificateDto certificate, string areaOfUsage, string holderName, DateTime expirationDate);
    }
}
