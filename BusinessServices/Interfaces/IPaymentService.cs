using BusinessEntites.Entities;
using BusinessServices.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IPaymentService
    {
        Task<int> ProcessPayment(PaymentDTO entity);
        Task<PaymentDTO> GetPaymentById(int Id);
        Task<List<CardDetails>> GetCardDetails();
        Task<List<PaymentDTO>> GetAllPayments();
        Task<bool> UpdatePayment(List<PaymentDTO> entityList);
        Task<bool> UpdatePaymentById(int Id , PaymentDTO entity);
    }
}
