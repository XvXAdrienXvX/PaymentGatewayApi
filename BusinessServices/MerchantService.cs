using BusinessServices.DTO;
using BusinessServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices
{
    public class MerchantService : IMerchantService
    {
        public IEnumerable<MerchantDTO> GetAllMerchants()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PaymentDTO> GetAllPayments()
        {
            throw new NotImplementedException();
        }

        public MerchantDTO GetMerchantById(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
