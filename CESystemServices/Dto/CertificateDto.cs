using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CESystemServicesExtensibility.Dto;

namespace CESystemServices.Dto
{
    public class CertificateDto : ICertificateDto
    {
        public int Id { get; set; }
        public string HolderName { get; set; }
        public string IssuerName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte[] SharedKey { get; set; }
        public byte[] Signature { get; set; }
        public string AreaOfUsage { get; set; }
        public string HashingAlgorithm { get; set; }
    }
}
