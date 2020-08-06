using BusinessEntites.Entities;
using System.Threading.Tasks;

namespace BusinessServices.Interfaces
{
    public interface IPaymentService
    {
        Task<int> ProcessPayment(Payment entity);
    }
}
