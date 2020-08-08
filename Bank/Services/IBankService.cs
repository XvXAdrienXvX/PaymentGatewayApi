using Bank.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.Services
{
    public interface IBankService
    {
        Task<List<PaymentDTO>> CapturePaymentAsync();
    }
}
