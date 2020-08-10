using System;
using System.Collections.Generic;

namespace BusinessEntites.Entities
{
    public partial class CardType
    {
        public CardType()
        {
            CardDetails = new HashSet<CardDetails>();
        }

        public int CardTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CardDetails> CardDetails { get; set; }
    }
}
