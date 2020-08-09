using BusinessEntites.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessServices.DTO
{
    public class CardDetailsDTO
    {
        public int CardDetailsId { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Card Number Required")]
        [CardDetailsValidator]
        public string CardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual CardTypeDTO CardType { get; set; }
        public virtual CustomerDTO Customer { get; set; }
    }
}
