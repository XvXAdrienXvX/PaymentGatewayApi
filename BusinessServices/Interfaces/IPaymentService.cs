using BusinessEntites.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IPaymentService
    {
        Task<int> ProcessPayment(Payment entity);
        Task<Payment> GetPaymentById(object Id);
        Task<List<CardDetails>> GetCardDetails();
    }
}
