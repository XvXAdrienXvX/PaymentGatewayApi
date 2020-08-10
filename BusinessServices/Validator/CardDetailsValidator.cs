using BusinessEntites.Entities;
using BusinessServices.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessEntites.Validator
{
    public class CardDetailsValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var cardDetails = (CardDetailsDTO)validationContext.ObjectInstance;

            if (!Int64.TryParse(cardDetails.CardNumber, out long num))
                return new ValidationResult("Card number must contain integers only");

            if(cardDetails.CardNumber.Length < 16 || cardDetails.CardNumber.Length > 16)
                return new ValidationResult("Card number must contain integers only");

            return ValidationResult.Success;

        }
    }
}
