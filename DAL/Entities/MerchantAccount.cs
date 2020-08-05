using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class MerchantAccount
    {
        public int MerchantAccountId { get; set; }
        public int MerchantId { get; set; }
        public int Amount { get; set; }

        public virtual Merchant Merchant { get; set; }
    }
}
