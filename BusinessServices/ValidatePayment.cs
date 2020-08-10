using BusinessEntites.Entities;
using BusinessEntites.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public class ValidatePayment
    {
        public ValidatePayment() { }

        public Payment ProcessPayment(Payment entity)
        {
            CardDetails cardDetails = entity.GetType().GetProperty("CardDetails").GetValue(entity) as CardDetails;
            CardType cardType = cardDetails.GetType().GetProperty("CardType").GetValue(cardDetails) as CardType;

            if (Int64.TryParse(cardDetails.CardNumber, out long num))
            {
                if (CheckCardType(cardType))
                {
                    entity.Status = (int)PaymentStatus.Approved;
                }
            }
            else
            {
                throw new Exception("Invalid Card Number Format");
            }

            return entity;
        }

        private bool CheckCardType(CardType cardType)
        {
            if (Enum.TryParse(cardType.Name, out CardTypeEnum type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
