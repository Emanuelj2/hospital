using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Enums
{
    public enum AppointmentStatus
    {
        Scheduled,
        CheckedIn,
        InProgress,
        Completed,
        Cancelled,
        NoShow
    }
}
