using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Medical.entity
{
    public class Perscription
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int PerscribedByEnoployeeId { get; set; }
        public Employee? PerscribedBy { get; set; }

        public string MedicationName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;

        public DateTime StartDay { get; set; }
        public DateTime? EndDay { get; set; }

        public string? Instructions { get; set; }
        public int RefillsRemaining { get; set; } = 0;
        public PrescriptionStatus Status { get; set; } = PrescriptionStatus.Active;

    }
}
