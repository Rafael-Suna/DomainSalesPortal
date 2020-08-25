using Microsoft.VisualBasic.FileIO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainSalesPortalWeb.Services
{
    public static class RDapService
    {


        public static object SearchDomain(string domain)
        {
            if (string.IsNullOrEmpty(domain))
            return null;

            var client = new RestClient($"https://rdap.verisign.com/com/v1/domain/{domain}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return response.Content;

            }
            else
                return null;

  

        }
    }
}
