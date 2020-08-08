using System;
using System.Collections.Generic;

namespace BusinessEntites.Entities
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int CurrencyId { get; set; }
        public int MerchantId { get; set; }
        public int CardDetailsId { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
        public DateTime ProcessedDate { get; set; }

        public virtual CardDetails CardDetails { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Merchant Merchant { get; set; }
    }
}
