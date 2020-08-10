using BusinessEntites.Entities;
using BusinessServices.DTO;
using System.Collections.Generic;

namespace BusinessServices.Interfaces
{
    public interface ICustomerService
    {
        CustomerDTO GetCustomerById(int customerId);
        IEnumerable<CustomerDTO> GetAllCustomers();
        int CreatePayment(Payment entity);
    }
}
