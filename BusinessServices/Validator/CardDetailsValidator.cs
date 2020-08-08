using BusinessEntites.Entities;
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
            var cardDetails = (CardDetails)validationContext.ObjectInstance;

            if (cardDetails.CardNumber.GetType() != typeof(long))
                return new ValidationResult("Card number must contain integers only");

            return ValidationResult.Success;

        }
    }
}
