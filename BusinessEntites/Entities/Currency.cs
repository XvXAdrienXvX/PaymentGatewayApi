using System;
using System.Collections.Generic;

namespace BusinessEntites.Entities
{
    public partial class Currency
    {
        public Currency()
        {
            Payment = new HashSet<Payment>();
        }

        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }
    }
}
