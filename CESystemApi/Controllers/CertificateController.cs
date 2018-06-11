using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CESystemApi.Models;
using CESystemServicesExtensibility.Dto;
using CESystemServicesExtensibility.Services;
using Microsoft.AspNet.Identity;

namespace CESystemApi.Controllers
{
    public class CertificateController : ApiController
    {
        private readonly ICertificateService certificateService;

        public CertificateController(ICertificateService certificateService)
        {
            this.certificateService = certificateService;
        }

        [HttpGet]
        [Route("/")]
        public IEnumerable<CertificateShortInfo> GetAllCertificates()
        {
            return certificateService.GetAllCertificates(User.Identity.GetUserId()).Select(ToClientDto);
        }

        [HttpGet]
        [Route("/")]
        public CertificateDto GetCertificate(int id)
        {
            return certificateService.GetCertificateById(id);
        }

        [HttpGet]
        [Route("Delete")]
        public bool DeleteCertificate(int id)
        {
            var certificate = certificateService.GetCertificateById(id);
            if (certificate == null)
            {
                return true;
            }

            if (certificateService.IsCertificateBelongsToUser(User.Identity.GetUserId(), id))
            {
                return certificateService.RemoveCertificateById(id);
            }

            return false;
        }

        private CertificateShortInfo ToClientDto(CertificateDto certificate)
        {
            return new CertificateShortInfo
            {
                Id = certificate.Id,
                Name = $"\"{certificate.IssuerName}\":\"{certificate.HolderName}\", {certificate.ExpirationDate}",
                SecondaryCertifictes = certificate.Children.Select(ToClientDto)
            };
        }
    }
}
