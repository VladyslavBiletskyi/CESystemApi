using System;

namespace CESystemServicesExtensibility.Dto
{
    public interface ICertificateDto
    {
        int Id { get; set; }

        string HolderName { get; set; }

        string IssuerName { get; set; }

        DateTime ExpirationDate { get; set; }

        byte[] SharedKey { get; set; }

        byte[] Signature { get; set; }

        string AreaOfUsage { get; set; }

        string HashingAlgorithm { get; set; }
    }
}
