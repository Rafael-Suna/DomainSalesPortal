using DomainSalesPortalDataLayer;
using DomainSalesPortalDataLayer.Entities;
using DomainSalesPortalWeb.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DomainSalesPortalWeb.Services
{

    public class DomainControlService : BackgroundService
    {
      
        private IMemoryCache memoryCache;
        IConfiguration _configuration;
        UnitOfWork _uwo;
        int UserId = 0;
        public  List<Domain> DomainList;
        SignalRClient _client;

        public DomainControlService(IMemoryCache memoryCache, IConfiguration Configuration)
        {
            this.memoryCache = memoryCache;
            _configuration = Configuration;
            DomainList= new List<Domain>();

            string connectionString = _configuration.GetConnectionString("Constr");

            _uwo = new UnitOfWork(connectionString);
            _client = SignalRClient.GetInstance();
            _client.Connect();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
            
                CheckDomains();

                await Task.Delay(1000*10, stoppingToken);
            }

        }

        public void CheckDomains()
        {

            if (this.memoryCache.TryGetValue("UserId", out string cacheEntry))
            {
                if (cacheEntry!=null)
                {
                    UserId = Convert.ToInt32(cacheEntry.ToString());
                }
            
            }
            if (UserId == 0)
                return;

            var reslt = _uwo.DomainRepository.FindByCustomerId(UserId).ToList();

            foreach (var item in reslt)
            {
                if (DateTime.Now.Date >= item.ExpiredDate.Date)
                {
                    item.Class = "alert alert-success";



                }
                 else
                {
                    item.Class = "";
                }

        
            }

            _client.SendDomains(reslt);
            //if (this.memoryCache.TryGetValue("Domains", out List<Domain> DomainENtry))
            //{
            //    DomainENtry = new List<Domain>();
            //}

            //if (DomainENtry!=null)
            //{
            //    foreach (var item in DomainENtry)
            //    {
            //        if (DateTime.Now.Date > item.ExpiredDate.Date)
            //        {
            //            item.Class = "alert alert-success";



            //        }


            //        else
            //        {
            //            item.Class = "";
            //        }

            //        DomainENtry.Add(item);

            //        _domainHub.Send(DomainENtry);
            //    }


            //}









        }
    }

  
}
