using System.Collections.Generic;

namespace CESystemApi.Models
{
    public class CertificateShortInfo
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public IEnumerable<CertificateShortInfo> SecondaryCertifictes { get; set; }
    }
}