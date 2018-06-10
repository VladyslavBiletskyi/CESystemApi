using CESystemDomainExtensibility.Entities;

namespace CESystemDomain.Entities
{
    public class CertificateHash: BaseInstance<int>, ICertificatesHash
    {
        public ICertificate Certificate => ReferredCertificate;
        public byte[] Hash { get; set; }

        public Certificate ReferredCertificate { get; set; }
    }
}
