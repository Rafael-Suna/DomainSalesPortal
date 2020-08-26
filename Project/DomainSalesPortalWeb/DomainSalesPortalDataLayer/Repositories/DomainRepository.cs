using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DomainSalesPortalDataLayer.Entities;

namespace DomainSalesPortalDataLayer.Repositories
{
    public class DomainRepository : RepositoryBase, IDomainRepository
    {
        public DomainRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public void Add(Domain entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

           Connection.ExecuteScalar<int>(
                "INSERT INTO Domains(IsActive, CustomerId, Name,NS1,NS2,LastChange,ExpiredDate,IsFavourite) VALUES(@IsActive, @CustomerId, @Name,@NS1,@NS2,@LastChange,@ExpiredDate,@IsFavourite);",
                param: new { IsActive=true, CustomerId=entity.CustomerId, Name=entity.Name, NS1=entity.NS1, NS2= entity.NS2, LastChange=entity.LastChange, ExpiredDate=entity.ExpiredDate, IsFavourite=true },
                transaction: Transaction
            );
        }

        public Domain Find(int id)
        {
            return Connection.Query<Domain>(
                  "SELECT * FROM Domains where id = @DomainId",
                  param: new { DomainId = id },
                  transaction: Transaction
              ).FirstOrDefault();
        }

        public IEnumerable<Domain> FindByCustomerId(int customerId)
        {
            return Connection.Query<Domain>(
                "SELECT * FROM Domains where CustomerId = @CustomerId and IsActive = 1",
                param: new { CustomerId = customerId },
                transaction: Transaction
            ).ToList();
        }

        public void Remove(int id)
        {
            Connection.Execute(
                "UPDATE Domains SET IsActive = @IsActive, IsFavourite=@IsFavourite WHERE id = @DomainId",
                param: new { IsActive = false, IsFavourite = false, DomainId = id},
                transaction: Transaction
            );
        }


    }
}
