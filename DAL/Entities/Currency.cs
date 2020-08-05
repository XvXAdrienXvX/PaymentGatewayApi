using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Currency
    {
        public Currency()
        {
            Payment = new HashSet<Payment>();
        }

        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }
    }
}
