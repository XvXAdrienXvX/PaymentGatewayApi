using AutoMapper;
using BusinessServices.DTO;
using BusinessServices.Interfaces;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BusinessServices
{
    public class CustomerService : ICustomerService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public int CreatePayment(PaymentDTO entity)
        {
            // guarantees roll back as a single unit
            using (var scope = new TransactionScope())
            {
                var payment = new Payment
                {
                   Amount = entity.Amount
                };
                _unitOfWork.PaymentRepository.Insert(payment);
                _unitOfWork.Save();
                scope.Complete();

                return payment.PaymentId;
            }
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public CustomerDTO GetCustomerById(int customerId)
        {
            var customer = _unitOfWork.CustomerRepository.GetByID(customerId);
            if (customer != null)
            {
                var customerModel = _mapper.Map<Customer, CustomerDTO>(customer);
                return customerModel;
            }
            return null;
        }
    }
}
