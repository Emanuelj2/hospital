using hospital.domain.Billing.entity;
using hospital.domain.Medical.entity;
using hospital.domain.Organization.entity;
using hospital.domain.People;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace hospital.infrastructure.Data
{
    public class HospitalDbContext: DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options){}

        //DbSets
        #region 
        //people
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Visitor> Visitors { get; set; }


        //organization
        public DbSet<Department> Department { get; set; }


        //medical
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }


        //billing
        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        #endregion


        //OnModelCreating
        #region
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Visitors)
                .WithMany(v => v.VisitingPatients);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Head)
                .WithMany()
                .HasForeignKey(d => d.HeadEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Deleting a Patient should be blocked while medical/financial records exist
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LabResult>()
                .HasOne(l => l.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Patient)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor leaving shouldn't delete patients — just clear the assignment
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.AssignedDoctor)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            // Deleting an Invoice can reasonably cascade to its claims
            modelBuilder.Entity<InsuranceClaim>()
                .HasOne(c => c.Invoice)
                .WithMany(i => i.Claims)
                .OnDelete(DeleteBehavior.Cascade);

            //unique constraints
            modelBuilder.Entity<UserAccount>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.MedicalRecordNumber)
                .IsUnique();

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.InvoiceNumber)
                .IsUnique();

            modelBuilder.Entity<InsuranceClaim>()
                .HasIndex(c => c.ClaimNumber)
                .IsUnique();
        }

        #endregion

    }
}
