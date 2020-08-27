using DomainSalesPortalDataLayer.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainSalesPortalWeb.Services
{
    public class SignalRClient
    {
        //private string url = "http://192.168.1.10:5400/driverhub";
        private string url = "https://localhost:44325/domainHubs";

        HubConnection Connection;

        public delegate void Error();


        public event Error ConnectionError;
        public async Task Connect()
        {

            Connection = new HubConnectionBuilder().WithUrl(url).Build();

            //Connection.On<SoforMesajModel>("ReceiveMessage", (_sendMessage) =>
            //{
            //    var sendMessage = new SendMessageModel
            //    {
            //        AdSoyad = "fatih baytar",
            //        Latitude = 41.2,
            //        Longitude = 42.2,
            //        SoforId = 1
            //    };
            //    OnmessageReceived?.Invoke(sendMessage);
            //    Mesaj alındığında yapılacak işlemler buraya yazılacak. Şu an için mesaj alma işlemleri yok.

            //});


            await Connection.StartAsync();
      
        }

      

        public async void SendDomains(List<Domain> sendMessage)
        {
            await Connection.InvokeAsync("SendDomains", sendMessage);
        }
        private SignalRClient()
        {

        }
        private static SignalRClient _instanse;
        public static SignalRClient GetInstance()
        {
            if (_instanse == null)
            {
                _instanse = new SignalRClient();
            }

            return _instanse;
        }
        public async Task Start()
        {
            await Connection.StartAsync();
        }

        public HubConnectionState State()
        {
            return Connection.State;
        }
        public HubConnection GetConnection()
        {
            return Connection;
        }

    }
}
