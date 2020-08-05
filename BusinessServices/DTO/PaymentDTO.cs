using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int CurrencyId { get; set; }
        public int CardDetailsId { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }

        public virtual CardDetailsDTO CardDetails { get; set; }
    }
}
