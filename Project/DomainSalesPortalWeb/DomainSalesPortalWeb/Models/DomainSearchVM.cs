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
        //[JsonPropertyName("ldhName")]
        //public string DomainName { get; set; }



        //[JsonPropertyName("events / eventDate")]
        //public List<DateTime> EventDates { get; set; }


        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 



        
        public string objectClassName { get; set; }
        public string handle { get; set; }
        public string ldhName { get; set; }
        public List<string> status { get; set; }
        public List<Event> events { get; set; }
        public SecureDNS secureDNS { get; set; }
        public List<Link> links { get; set; }
        public List<Nameserver> nameservers { get; set; }
        public List<string> rdapConformance { get; set; }
        public List<Notice> notices { get; set; }
        public List<Entity> entities { get; set; }
    


        public class Event
        {
            public string eventAction { get; set; }
            public DateTime eventDate { get; set; }
        }

        public class SecureDNS
        {
            public bool delegationSigned { get; set; }
        }

        public class Link
        {
            public string value { get; set; }
            public string rel { get; set; }
            public string href { get; set; }
            public string type { get; set; }
        }

        public class Nameserver
        {
            public string objectClassName { get; set; }
            public string ldhName { get; set; }
        }

        public class Link2
        {
            public string href { get; set; }
            public string type { get; set; }
        }

        public class Notice
        {
            public string title { get; set; }
            public List<string> description { get; set; }
            public List<Link2> links { get; set; }
        }

        public class PublicId
        {
            public string type { get; set; }
            public string identifier { get; set; }
        }

        public class Entity2
        {
            public string objectClassName { get; set; }
            public List<string> roles { get; set; }
            public List<object> vcardArray { get; set; }
        }

        public class Remark
        {
            public string title { get; set; }
            public string type { get; set; }
            public List<string> description { get; set; }
        }

        public class Entity
        {
            public string objectClassName { get; set; }
            public string handle { get; set; }
            public List<string> roles { get; set; }
            public List<PublicId> publicIds { get; set; }
            public List<object> vcardArray { get; set; }
            public List<Entity2> entities { get; set; }
            public List<Remark> remarks { get; set; }
        }

   


    }
}
