using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Users
    {
        public Users()
        {
            CardDetails = new HashSet<CardDetails>();
            Customer = new HashSet<Customer>();
            Merchant = new HashSet<Merchant>();
            Payment = new HashSet<Payment>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CardDetails> CardDetails { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Merchant> Merchant { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
