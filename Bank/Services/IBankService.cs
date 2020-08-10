using Bank.DTO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Bank.Services
{
    public interface IBankService
    {
        Task<List<PaymentDTO>> CapturePaymentAsync();
        Task<HttpStatusCode> PutPayment(List<PaymentDTO> entity);
    }
}
