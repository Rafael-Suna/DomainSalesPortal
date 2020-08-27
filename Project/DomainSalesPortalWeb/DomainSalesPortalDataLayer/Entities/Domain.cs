using System;
using System.Collections.Generic;
using System.Text;

namespace DomainSalesPortalDataLayer.Entities
{
  public  class Domain
    {

        public int Id { get; set; }

        public bool IsActive { get; set; }
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string NS1 { get; set; }

        public string NS2 { get; set; }
      
        public DateTime LastChange { get; set; }

        public DateTime ExpiredDate { get; set; }

        public bool IsFavourite { get; set; }

        public string Class { get; set; }

    }
}
