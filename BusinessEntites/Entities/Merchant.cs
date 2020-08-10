using System;
using System.Collections.Generic;

namespace BusinessEntites.Entities
{
    public partial class Merchant
    {
        public Merchant()
        {
            MerchantAccount = new HashSet<MerchantAccount>();
            Payment = new HashSet<Payment>();
        }

        public int MerchantId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MerchantAccount> MerchantAccount { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
