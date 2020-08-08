using BusinessEntites.Entities;
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

        public PaymentService()
        {
            _unitOfWork = new UnitOfWork();
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

        public async Task<Payment> GetPaymentByCustomerId(object Id)
        {
            var paymentDetails = await _unitOfWork.PaymentRepository.GetByIDAsync(Id);
            var cardDetails = await _unitOfWork.CardRepository.GetByIDAsync(Id);
            if (paymentDetails != null)
            {
                paymentDetails.CardDetails = cardDetails;
                return paymentDetails;
            }
            return null;
        }

        public async Task<int> ProcessPayment(Payment entity)
        {
            CardDetails cardDetails = entity.CardDetails;
            // guarantees roll back as a single unit
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var payment = new Payment
                {
                    UserId = entity.UserId,
                    CurrencyId = entity.CurrencyId,
                    OrderId = entity.OrderId,
                    Amount = entity.Amount,
                    Status = entity.Status,
                    ProcessedDate = entity.ProcessedDate,
                    CardDetails = new CardDetails
                    {
                        UserId = cardDetails.UserId,
                        CustomerId = cardDetails.CustomerId,
                        CardNumber = cardDetails.CardNumber,
                        CardTypeId = cardDetails.CardTypeId,
                        Cvv = cardDetails.Cvv,
                        ExpiryDate = cardDetails.ExpiryDate
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
