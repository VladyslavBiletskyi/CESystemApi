using System;
using System.Collections.Generic;

namespace CESystemServicesExtensibility.Dto
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public string HolderName { get; set; }
        public string IssuerName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte[] SharedKey { get; set; }
        public byte[] Signature { get; set; }
        public string AreaOfUsage { get; set; }
        public string HashingAlgorithm { get; set; }

        public IEnumerable<CertificateDto> Children { get; set; }
    }
}
