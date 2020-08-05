using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public partial class Merchant
    {
        public Merchant()
        {
            MerchantAccount = new HashSet<MerchantAccount>();
        }

        public int MerchantId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<MerchantAccount> MerchantAccount { get; set; }
    }
}
