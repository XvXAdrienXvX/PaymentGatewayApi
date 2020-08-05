using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public int CardDetailsId { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }

        public virtual CardDetails CardDetails { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Users User { get; set; }
    }
}
