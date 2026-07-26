using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Medical.entity
{
    public class LabResult
    {
        public int Id { get; set; }

        public int PatientId { get; set; } 
        public Patient? Patient { get; set; }

        public int OrderedByEmployeeId { get; set; } 
        public Employee? OrderedBy { get; set; }

        public string TestName { get; set; } = string.Empty;
        public DateTime TestDate { get; set; }
        public DateTime? ResultDate { get; set; }

        public string? ResultValue { get; set; }
        public string? Units { get; set; }
        public string? ReferenceRange { get; set; }
        public bool IsAbnormal { get; set; } = false;

        public string? Notes { get; set; }

        public LabResultStatus Status { get; set; } = LabResultStatus.Pending;
    }
}
