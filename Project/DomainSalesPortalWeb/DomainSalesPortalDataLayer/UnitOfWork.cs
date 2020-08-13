/*  Written by Tim Schreiber
    StackOverflow user 'sakir' is incorrectly claiming that they wrote this code in the following answer: 
        http://stackoverflow.com/questions/31298235/dapper-and-unit-of-work-pattern/31636037
    
    They have never in any way contributed to this code, and the false attribution has been reported to StackOverflow. */

using CustomerSalesPortalDataLayer.Repositories.Customer;
using DomainSalesPortalDataLayer.Repositories;
using DomainSalesPortalDataLayer.Repositories.Customer;
using DomainSalesPortalDataLayer.Repositories.Domain;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DomainSalesPortalDataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
 
        private IDomainRepository _domainRepository;

        private ICustomerRepository _customerRepository;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

   

        public IDomainRepository DomainRepository
        {
            get { return _domainRepository ?? (_domainRepository = new DomainRepository(_transaction)); }
        }

        public ICustomerRepository CustomerRepository
        {
            get { return _customerRepository ?? (_customerRepository = new CustomerRepository(_transaction)); }
        }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
          
            _domainRepository = null;
            _customerRepository = null;

        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if(disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if(_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
