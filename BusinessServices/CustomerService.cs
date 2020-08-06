using AutoMapper;
using BusinessEntites.Entities;
using BusinessServices.DTO;
using BusinessServices.Interfaces;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
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

        public int CreatePayment(Payment entity)
        {
            // guarantees roll back as a single unit
            using (var scope = new TransactionScope())
            {
                var payment = new Payment
                {
                   UserId = entity.UserId,
                   CurrencyId = entity.CurrencyId,
                   Amount = entity.Amount,
                   Status = entity.Status,
                   CardDetails = new CardDetails
                   {
                       UserId = (int)entity.CardDetails.GetType().GetProperty("UserId").GetValue(entity.CardDetails),
                       CardNumber = (int)entity.CardDetails.GetType().GetProperty("CardNumber").GetValue(entity.CardDetails),
                       CardTypeId = (int)entity.CardDetails.GetType().GetProperty("CardTypeId").GetValue(entity.CardDetails),
                       Cvv = (int)entity.CardDetails.GetType().GetProperty("Cvv").GetValue(entity.CardDetails),
                       ExpiryDate = (DateTime)entity.CardDetails.GetType().GetProperty("ExpiryDate").GetValue(entity.CardDetails)
                   }
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
