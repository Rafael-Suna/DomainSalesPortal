using System;
using System.Collections.Generic;
using System.Text;

namespace DomainSalesPortalDataLayer.Entities
{
   public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegisteredDate { get; set; }
    }
}
