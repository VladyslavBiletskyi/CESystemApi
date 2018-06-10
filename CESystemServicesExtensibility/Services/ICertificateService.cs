using CESystemServicesExtensibility.Dto;

namespace CESystemServicesExtensibility.Services
{
    public interface ICertificateService
    {
        ICertificateDto GetCertificateById(int id);

        bool UpdateCertificate(ICertificateDto certificate);

        bool RemoveCertificateById(int id);
    }
}
