using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Enums
{
    public enum InvoiceStatus
    {
        Draft,
        Sent,
        PartiallyPaid,
        Paid,
        Overdue,
        Cancelled
    }
}
