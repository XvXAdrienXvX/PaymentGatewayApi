using BusinessServices.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.Interfaces
{
    public interface IMerchantService
    {
        MerchantDTO GetMerchantById(int customerId);
        IEnumerable<MerchantDTO> GetAllMerchants();
        IEnumerable<PaymentDTO> GetAllPayments();
    }
}
