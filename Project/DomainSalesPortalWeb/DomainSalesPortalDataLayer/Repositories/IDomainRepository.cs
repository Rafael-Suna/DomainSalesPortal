using DomainSalesPortalDataLayer.Entities;
using System.Collections.Generic;


namespace DomainSalesPortalDataLayer.Repositories
{
    public interface IDomainRepository
    {
        void Add(Domain entity);
        Domain Find(int id);
        IEnumerable<Domain> FindByCustomerId(int customerId);
        void Remove(int id);
   
    }
}