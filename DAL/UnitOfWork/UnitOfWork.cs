using DAL.Context;
using DAL.Entities;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private PaymentDBContext _paymentDBContext = new PaymentDBContext();
        private IGenericRepository<Customer> _customerRepository;
        private IGenericRepository<Merchant> _merchantRepository;
        private IGenericRepository<Payment> _paymentRepository;
        private IGenericRepository<Users> _userRepository;

        #region Repositories
        public IGenericRepository<Customer> CustomerRepository
        {
            get
            {
                return _customerRepository ?? (_customerRepository = new GenericRepository<Customer>(_paymentDBContext));
            }
        }

        public IGenericRepository<Merchant> MerchantRepository
        {
            get
            {
                return _merchantRepository ?? (_merchantRepository = new GenericRepository<Merchant>(_paymentDBContext));
            }
        }

        public IGenericRepository<Payment> PaymentRepository
        {
            get
            {
                return _paymentRepository ?? (_paymentRepository = new GenericRepository<Payment>(_paymentDBContext));
            }
        }

        public IGenericRepository<Users> UserRepository
        {
            get
            {
                return _userRepository ?? (_userRepository = new GenericRepository<Users>(_paymentDBContext));
            }
        }
        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_paymentDBContext != null)
                {
                    _paymentDBContext.Dispose();
                    _paymentDBContext = null;
                }
            }
        }
    }
}
