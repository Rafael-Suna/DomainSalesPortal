using DomainSalesPortalDataLayer.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainSalesPortalWeb.Hubs
{
    public class DomainHubs : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async void SendDomains(List<Domain> AviableToBuyDomains)
        {   
           await Clients.All.SendAsync("ReceiveDomains",AviableToBuyDomains);

  
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
          
            return base.OnDisconnectedAsync(exception);
        }
    }
}
