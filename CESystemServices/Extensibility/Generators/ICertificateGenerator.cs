using System;
using CESystemDomainExtensibility.Entities;

namespace CESystemServices.Extensibility.Generators
{
    public interface ICertificateGenerator
    {
        ICertificate GenerateChildCertificate(ICertificate parent, string areaOfUsage, string holderName, DateTime expirationDate);
    }
}
