﻿using CESystemServicesExtensibility.Dto;

namespace CESystemServicesExtensibility.Services
{
    public interface ICertificateService
    {
        CertificateDto GetCertificateById(int id);

        bool UpdateCertificate(CertificateDto certificate);

        bool RemoveCertificateById(int id);

        bool AddCertificate(UserDto user, CertificateDto certificate);
    }
}
