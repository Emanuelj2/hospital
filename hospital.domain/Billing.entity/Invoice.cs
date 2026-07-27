using hospital.domain.Enums;
using hospital.domain.People;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.domain.Billing.entity
{
    public class Invoice
    {
        public int Id { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public int PatientId { get; set; } // Foreign key to Patient
        public Patient? Patient { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }

        [Precision(10, 2)]
        public decimal TotalAmount { get; set; }
        [Precision(10, 2)]
        public decimal AmountPaid { get; set; } = 0;

        public string? Description { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        public List<InsuranceClaim> Claims { get; set; } = new();
    }
}
