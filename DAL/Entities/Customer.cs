using System;
using System.Collections.Generic;

namespace DAL.Entities
{
    public partial class Customer
    {
        public int CustomerD { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public virtual Users User { get; set; }
    }
}
