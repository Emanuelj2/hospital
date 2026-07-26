using hospital.domain.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Billing.entity
{
    public class InsuranceClaim
    {
        public int Id { get; set; }

        public string ClaimNumber { get; set; } = string.Empty;

        public int PatientId { get; set; } // Foreign key to Patient
        public Patient? Patient { get; set; }

        public int InvoiceId { get; set; } // Foreign key to Invoice
        public Invoice? Invoice { get; set; }

        public string InsuranceProvider { get; set; } = string.Empty;
        public string PolicyNumber { get; set; } = string.Empty;

        public decimal ClaimAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }

        public DateTime SubmissionDate { get; set; }
        public DateTime? ResolutionDate { get; set; }

        public string? DenialReason { get; set; }

        public ClaimStatus Status { get; set; } = ClaimStatus.Submitted;
    }
}
