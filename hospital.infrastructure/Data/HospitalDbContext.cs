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

        #region //DbSets
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
            
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

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

            // A UserAccount is optional for both Employee and Patient — losing the login
            // shouldn't take the person's record with it.
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.UserAccount)
                .WithMany()
                .HasForeignKey(e => e.UserAccountId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.UserAccount)
                .WithMany()
                .HasForeignKey(p => p.UserAccountId)
                .OnDelete(DeleteBehavior.SetNull);

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

            // A UserAccount identifies exactly one person — prevent two Employees (or an
            // Employee and a Patient) from ever being linked to the same login.
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.UserAccountId)
                .IsUnique()
                .HasFilter("[UserAccountId] IS NOT NULL");

            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.UserAccountId)
                .IsUnique()
                .HasFilter("[UserAccountId] IS NOT NULL");

            //enum conversions — store enums as their name so reordering/inserting values
            //later can't silently reinterpret already-persisted rows.
            modelBuilder.Entity<UserAccount>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Job)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmploymentType)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.AccessLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Pronoun)
                .HasConversion<string>();

            modelBuilder.Entity<Patient>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Patient>()
                .Property(p => p.BloodType)
                .HasConversion<string>();

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Status)
                .HasConversion<string>();

            modelBuilder.Entity<InsuranceClaim>()
                .Property(c => c.Status)
                .HasConversion<string>();

            modelBuilder.Entity<LabResult>()
                .Property(l => l.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Prescription>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Visitor>()
                .Property(v => v.Relationship)
                .HasConversion<string>();
        }

        #endregion

    }
}
