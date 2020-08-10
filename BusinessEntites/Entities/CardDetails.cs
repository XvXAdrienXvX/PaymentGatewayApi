using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntites.Entities
{
    public partial class CardDetails
    {
        public CardDetails()
        {
            Payment = new HashSet<Payment>();
        }

        public int CardDetailsId { get; set; }
        public int CardTypeId { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Card Number Required")]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual CardType CardType { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
