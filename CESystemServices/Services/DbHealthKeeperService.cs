using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CESystemDomainExtensibility.Entities;
using CESystemDomainExtensibility.Repositories;
using CESystemServicesExtensibility.Services;
using Quartz;
using Quartz.Impl;

namespace CESystemServices.Services
{
    public class DbHealthKeeperService : IDbHealthKeeperService, IJob
    {
        private readonly ICertificatesHashRepository certificatesHashRepository;
        private readonly ICertificateRepository certificateRepository;

        public DbHealthKeeperService(ICertificatesHashRepository certificatesHashRepository, ICertificateRepository certificateRepository)
        {
            this.certificatesHashRepository = certificatesHashRepository;
            this.certificateRepository = certificateRepository;
        }

        public void Run()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start().RunSynchronously();

            IJobDetail job = JobBuilder.Create<DbHealthKeeperService>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("CEApiHealthCheck", "CEApi")     
                .StartNow()                           
                .WithSimpleSchedule(x => x            
                    .WithIntervalInHours(4)          
                    .RepeatForever())                  
                .Build();                              

            scheduler.ScheduleJob(job, trigger).RunSynchronously();
        }

        private IEnumerable<ICertificate> FindChangedCertificates()
        {
            var result = new LinkedList<ICertificate>();
            var certificatesHashes = certificatesHashRepository.GetAllInstances().ToList();
            foreach (var certificate in certificateRepository.GetAllInstances().ToList())
            {
                var certificateHash = certificatesHashes.FirstOrDefault(x => x.Certificate.Id == certificate.Id);
                if (certificateHash == null)
                {
                    result.AddLast(certificate);
                }
                else
                {
                    if (certificateHash.Hash != certificate.Hash)
                    {
                        result.AddLast(certificate);
                    }
                }
            }
            return result;
        }

        private void RestoreCertificateFromDump(ICertificate certificate)
        {
            throw new System.NotImplementedException();
        }

        public void CreateDbDump(IEnumerable<ICertificate> changedCertificates)
        {
            if (changedCertificates.Any())
            {

            }
        }

        public Task Execute(IJobExecutionContext context)
        {
            var changedCertificates = FindChangedCertificates().ToList();
            foreach (var changedCertificate in changedCertificates)
            {
                if (changedCertificate.IsCertificateValid())
                {
                    var hash = certificatesHashRepository.GetAllInstances()
                        .FirstOrDefault(x => x.Certificate.Id == changedCertificate.Id);
                    if (hash != null)
                    {
                        hash.Hash = changedCertificate.Hash;
                        certificatesHashRepository.UpdateInstance(hash);

                    }
                    else
                    {
                        certificatesHashRepository.AddCertificateHash(changedCertificate);
                    }
                }
                else
                {
                    RestoreCertificateFromDump(changedCertificate);
                }
            }
            CreateDbDump(changedCertificates);
            return Task.CompletedTask;
        }
    }
}