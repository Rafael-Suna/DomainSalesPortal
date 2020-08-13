using DomainSalesPortalDataLayer.Entities;
using System.Collections.Generic;


namespace CustomerSalesPortalDataLayer.Repositories
{
    public interface ICustomerRepository
    {

        Customer Login(string email, string password);

        void Add(Customer entity);
    
        Customer Find(int id);
     
    }
}