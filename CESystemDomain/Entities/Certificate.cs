using System;
using System.Collections.Generic;
using CESystemDomainExtensibility.Entities;

namespace CESystemDomain.Entities
{
    public class Certificate : BaseInstance<int>, ICertificate
    {
        public virtual User Owner { get; set; }
        public ICertificate ParentCertificate => ParentInternal;

        public virtual IUser User => Owner;

        public byte[] Hash => GetHash();
        public string HolderName { get; set; }
        public string IssuerName { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte[] SharedKey { get; set; }
        public byte[] Signature { get; set; }
        public string AreaOfUsage { get; set; }
        public string HashingAlgorithm { get; set; }

        public bool IsCertificateValid()
        {
            return true;
        }

        public IEnumerable<ICertificate> Children => ChildrenInternal;

        public Certificate ParentInternal { get; set; }

        public virtual ICollection<Certificate> ChildrenInternal { get; set; }

        private byte[] GetHash()
        {
            return new byte[0];
        }
    }
}
