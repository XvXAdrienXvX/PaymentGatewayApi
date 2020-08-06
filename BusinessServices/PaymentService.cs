using BusinessEntites.Entities;
using BusinessServices.Interfaces;
using DAL.UnitOfWork;
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
                    CustomerId = entity.CustomerId,
                    OrderId = entity.OrderId,
                    Amount = entity.Amount,
                    Status = entity.Status,
                    ProcessedDate = entity.ProcessedDate,
                    CardDetails = new CardDetails
                    {
                        UserId = cardDetails.UserId,
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
