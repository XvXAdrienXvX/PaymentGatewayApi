using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Bank.Enums
{
    public enum CurrencyEnum : int
    {
        [Description("USD")]
        UNITED_STATES_DOLLARS = 1,

        [Description("MUR")]
        MAURITIAN_RUPEES = 2,

        [Description("CAD")]
        CANADIAN_DOLLARS = 3,

        [Description("GBD")]
        POUND_STERLING = 4
    }
}
