using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DomainSalesPortalWeb.Models
{
    public class DomainSearchVM
    {
        [JsonPropertyName("ldhName")]
        public string DomainName { get; set; }



        [JsonPropertyName("events / eventDate")]
        public List<DateTime> EventDates { get; set; }



    }
}
