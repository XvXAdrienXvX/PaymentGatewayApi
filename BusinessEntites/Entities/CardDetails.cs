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

        public int UserId { get; set; }

        public int CardTypeId { get; set; }

        [Required(ErrorMessage = "Card Number is required")]
        [DataType(DataType.CreditCard, ErrorMessage = "Invalid card number:Only integer allowed. Number must be 14 integers")] 
        public long CardNumber { get; set; }

        [Required(ErrorMessage = "Cvv is required")]
        public int Cvv { get; set; }

        [Required(ErrorMessage = "ExpiryDate is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        public virtual CardType CardType { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
