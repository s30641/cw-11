using Cwiczenia11.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia11.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for PrescriptionMedicament
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

            // Prescription - Patient relationship
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(patient => patient.Prescriptions)
                .HasForeignKey(p => p.IdPatient);

            // Prescription - Doctor relationship
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(doctor => doctor.Prescriptions)
                .HasForeignKey(p => p.IdDoctor);

            // PrescriptionMedicament - Prescription relationship
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Prescription)
                .WithMany(prescription => prescription.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdPrescription);

            // PrescriptionMedicament - Medicament relationship
            modelBuilder.Entity<PrescriptionMedicament>()
                .HasOne(pm => pm.Medicament)
                .WithMany(medicament => medicament.PrescriptionMedicaments)
                .HasForeignKey(pm => pm.IdMedicament);

            base.OnModelCreating(modelBuilder);
        }
    }
}