using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.DTO
{
    public class PaymentDTO
    {
        public int MerchantId { get; set; }
        public int OrderId { get; set; }
        public int CardDetailsId { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
        public DateTime ProcessedDate { get; set; }

        public virtual CardDetailsDTO CardDetails { get; set; }
        public virtual CurrencyDTO Currency { get; set; }
        public virtual MerchantDTO Merchant { get; set; }
    }
}
