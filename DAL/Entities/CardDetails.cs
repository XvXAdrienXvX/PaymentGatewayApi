using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class CardDetails
    {
        public CardDetails()
        {
            Payment = new HashSet<Payment>();
        }

        public int CardDetailsId { get; set; }
        public int UserId { get; set; }
        public int CardTypeId { get; set; }
        public int CardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual CardType CardType { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
