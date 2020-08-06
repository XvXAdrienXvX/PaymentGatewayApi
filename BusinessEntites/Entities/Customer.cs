using System;
using System.Collections.Generic;

namespace BusinessEntites.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Payment = new HashSet<Payment>();
        }

        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
