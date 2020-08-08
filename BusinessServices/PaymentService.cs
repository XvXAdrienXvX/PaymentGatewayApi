using AutoMapper;
using BusinessEntites.Entities;
using BusinessServices.DTO;
using BusinessServices.Interfaces;
using DAL.UnitOfWork;
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

        public PaymentService(IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            var paymentList = await _unitOfWork.PaymentRepository.GetAll();
            var paymentListDTO = _mapper.Map<List<Payment>, List<PaymentDTO>>(paymentList);
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

        public async Task<PaymentDTO> GetPaymentByCustomerId(object Id)
        {
            var paymentDetails = await _unitOfWork.PaymentRepository.GetByIDAsync(Id);
            var cardDetails = await _unitOfWork.CardRepository.GetAll();
            var paymentDetailsDTO = _mapper.Map<Payment, PaymentDTO>(paymentDetails);
            var cardDetailsDTO = _mapper.Map<List<CardDetails>, List<CardDetailsDTO>>(cardDetails);

            if (paymentDetailsDTO != null)
            {
                paymentDetailsDTO.CardDetails = cardDetailsDTO.Where(x => x.CardDetailsId == paymentDetails.CardDetailsId).FirstOrDefault();
                return paymentDetailsDTO;
            }
            return null;
        }

        public async Task<int> ProcessPayment(PaymentDTO entity)
        {
            Payment paymentEntity = _mapper.Map<PaymentDTO, Payment>(entity);
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
    }
}
