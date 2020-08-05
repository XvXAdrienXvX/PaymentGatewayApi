using BusinessEntities.Entities;
using BusinessServices.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.Interfaces
{
    public interface ICustomerService
    {
        CustomerDTO GetCustomerById(int customerId);
        IEnumerable<CustomerDTO> GetAllCustomers();
        int CreatePayment(Payment entity);
    }
}
