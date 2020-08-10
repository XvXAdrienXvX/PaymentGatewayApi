using AutoMapper;
using BusinessEntites.Entities;
using BusinessEntites.Enums;
using BusinessServices.DTO;
using BusinessServices.Helpers;
using BusinessServices.Interfaces;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessServices
{
    public class PaymentService : IPaymentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private HelperUtilities _helperUtilities;

        public PaymentService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
            _helperUtilities = new HelperUtilities();
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            var paymentList = await _unitOfWork.PaymentRepository.GetAll();
            var paymentListDTO = _mapper.Map<List<Payment>, List<PaymentDTO>>(paymentList);
            var cardDetails = await _unitOfWork.CardRepository.GetAll();
            var cardDetailsDTO = _mapper.Map<List<CardDetails>, List<CardDetailsDTO>>(cardDetails);
            if(paymentListDTO.Any())
            {
                foreach(var payments in paymentListDTO)
                {
                    payments.CardDetails = cardDetailsDTO.Where(x => x.CardDetailsId == payments.CardDetailsId).FirstOrDefault();
                }
            }
            return paymentListDTO;
        }

        public async Task<List<CardDetails>> GetCardDetails()
        {
            var cardDetailsList = await _unitOfWork.CardRepository.GetAll();
            return cardDetailsList;
        }

        public async Task<CardDetails> GetCardDetailsByCustomerId(object Id)
        {
            var cardDetails = await _unitOfWork.CardRepository.GetByIDAsync(Id);
            if (cardDetails != null)
            {
                return cardDetails;
            }
            return null;
        }

        public async Task<PaymentDTO> GetPaymentById(int Id)
        {
            var paymentDetails = await _unitOfWork.PaymentRepository.GetByIDAsync(Id);
            var cardDetails = await _unitOfWork.CardRepository.GetAll();
            var paymentDetailsDTO = _mapper.Map<Payment, PaymentDTO>(paymentDetails);
            var cardDetailsDTO = _mapper.Map<List<CardDetails>, List<CardDetailsDTO>>(cardDetails);

            if (paymentDetailsDTO != null)
            {
                paymentDetailsDTO.CardDetails = cardDetailsDTO.Where(x => x.CardDetailsId == paymentDetails.CardDetailsId).FirstOrDefault();
                paymentDetailsDTO.CardDetails.CardNumber = _helperUtilities.MaskCardNumber(paymentDetailsDTO.CardDetails.CardNumber);
                return paymentDetailsDTO;
            }
            return null;
        }

        public async Task<int> ProcessPayment(PaymentDTO entity)
        {
            Payment paymentEntity = new ValidatePayment().ProcessPayment(_mapper.Map<PaymentDTO, Payment>(entity));
            CardDetails cardDetails = paymentEntity.CardDetails;
            Currency currency = paymentEntity.Currency;
            CardType cardType = cardDetails.CardType;

            // guarantees roll back as a single unit
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var payment = new Payment
                {
                    MerchantId = paymentEntity.MerchantId,
                    CurrencyId = paymentEntity.CurrencyId,
                    OrderId = paymentEntity.OrderId,
                    Amount = paymentEntity.Amount,
                    Status = paymentEntity.Status,
                    ProcessedDate = paymentEntity.ProcessedDate,
                    Currency = new Currency
                    {
                        Code = currency.Code
                    },
                    CardDetails = new CardDetails
                    {
                        CustomerId = cardDetails.CustomerId,
                        CardNumber = cardDetails.CardNumber,                       
                        Cvv = cardDetails.Cvv,
                        ExpiryDate = cardDetails.ExpiryDate,
                        CardType = new CardType
                        {
                            Name = cardType.Name
                        }
                    }
                };
                await _unitOfWork.PaymentRepository.Insert(payment);
                await _unitOfWork.Save();
                scope.Complete();

                return payment.PaymentId;
            }
        }

        public async Task<bool> UpdatePayment(List<PaymentDTO> entityList)
        {
            var success = false;
            if (entityList.Any() && entityList != null)
            {
                using (var scope = new TransactionScope())
                {
                   foreach(var item in entityList)
                   {
                        var payment = _unitOfWork.PaymentRepository.GetByID(item.PaymentId);
                        if (payment != null)
                        {
                            payment.Status = item.Status;
                             _unitOfWork.PaymentRepository.Update(payment);
                            await _unitOfWork.Save();
                            scope.Complete();
                            success = true;
                        }
                    }
                }
            }
            return success;
        }

        public async Task<bool> UpdatePaymentById(int paymentId, PaymentDTO entity)
        {
            var success = false;
            if (entity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var payment = _unitOfWork.PaymentRepository.GetByID(paymentId);
                    if (payment != null)
                    {
                        payment.Status = entity.Status;
                        _unitOfWork.PaymentRepository.Update(payment);
                        await _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
