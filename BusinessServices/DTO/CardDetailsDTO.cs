﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessServices.DTO
{
    public class CardDetailsDTO
    {
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public long CardNumber { get; set; }
        public int Cvv { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual CardTypeDTO CardType { get; set; }
        public virtual CustomerDTO Customer { get; set; }
    }
}
