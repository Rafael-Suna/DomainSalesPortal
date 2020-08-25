using CustomerSalesPortalDataLayer.Repositories;
using Dapper;
using DomainSalesPortalDataLayer.Entities;
using System;
using System.Data;
using System.Linq;

namespace DomainSalesPortalDataLayer.Repositories
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public Customer Login(string email, string password)
        {
            return Connection.Query<Customer>(
               "SELECT * FROM Customers WHERE Email = @Email and Password=@Password",
               param: new { Email = email , Password=password},
               transaction: Transaction
           ).FirstOrDefault();
        }


        public Customer Find(int id)
        {
            return Connection.Query<Customer>(
                "SELECT * FROM Customers WHERE DomainId = @DomainId",
                param: new { DomainId = id },
                transaction: Transaction
            ).FirstOrDefault();
        }

        public void Add(Customer entity)
        {
            if (entity == null)
                return;


            entity.Id = Connection.ExecuteScalar<int>(
                "INSERT INTO Customers(Name, Surname, FullName,Email,RegisteredDate,Password) VALUES(@Name, @Surname, @FullName,@Email,@RegisteredDate,@Password);",
                param: new { Name= entity.Name, Surname=entity.Surname, FullName=entity.FullName, Email=entity.Email, RegisteredDate=entity.RegisteredDate, Password=entity.Password },
                transaction: Transaction
            );
        }

        //public void Add(Customer entity)
        //{
        //    if (entity == null)
        //        throw new ArgumentNullException("entity");

        //    entity.Id = Connection.ExecuteScalar<int>(
        //        "INSERT INTO Domain(BreedId, Name, Age) VALUES(@BreedId, @Name, @Age); SELECT SCOPE_IDENTITY()",
        //        param: new { BreedId = entity.BreedId, Name = entity.Name, Age = entity.Age },
        //        transaction: Transaction
        //    );
        //}


    }
}
