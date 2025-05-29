using Cwiczenia11.Data;
using Cwiczenia11.DTOs;
using Cwiczenia11.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia11.Services
{
    public class DbService : IDbService
    {
        private readonly DatabaseContext _context;

        public DbService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> AddPrescription(CreatePrescriptionRequestDto request)
        {
            if (request.DueDate < request.Date)
                throw new ArgumentException("DueDate must be greater or equal to Date.");

            if (request.Medicaments.Count > 10)
                throw new ArgumentException("Recepta może obejmować maksymalnie 10 leków.");

            var doctor = await _context.Doctors.FindAsync(request.IdDoctor);
            if (doctor == null)
                throw new ArgumentException("Lekarz nie istnieje.");

            Patient patient;

            if (request.Patient.IdPatient.HasValue)
            {
                patient = await _context.Patients.FindAsync(request.Patient.IdPatient.Value);
                if (patient == null)
                {
                    patient = new Patient
                    {
                        FirstName = request.Patient.FirstName,
                        LastName = request.Patient.LastName,
                        Birthdate = request.Patient.Birthdate
                    };
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                patient = new Patient
                {
                    FirstName = request.Patient.FirstName,
                    LastName = request.Patient.LastName,
                    Birthdate = request.Patient.Birthdate
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            var prescription = new Prescription
            {
                IdDoctor = request.IdDoctor,
                IdPatient = patient.IdPatient,
                Date = request.Date,
                DueDate = request.DueDate,
                PrescriptionMedicaments = new List<PrescriptionMedicament>()
            };

            foreach (var med in request.Medicaments)
            {
                var medicament = await _context.Medicaments.FindAsync(med.idMedicament);
                if (medicament == null)
                    throw new ArgumentException($"Medicament with id {med.idMedicament} does not exist.");

                prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
                {
                    IdMedicament = med.idMedicament,
                    Dose = med.Dose,
                    Details = med.Description
                });
            }

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return prescription.IdPrescription;
        }

        public async Task<PatientDetailsDto> GetPatientDetails(int idPatient)
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.PrescriptionMedicaments)
                        .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == idPatient);

            if (patient == null)
                throw new ArgumentException("Patient not found.");

            return new PatientDetailsDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate,
                Prescriptions = patient.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new PrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName,
                            Email = pr.Doctor.Email
                        },
                        Medicaments = pr.PrescriptionMedicaments
                            .Select(pm => new MedicamentDto
                            {
                                IdMedicament = pm.Medicament.IdMedicament,
                                Name = pm.Medicament.Name,
                                Dose = pm.Dose,
                                Description = pm.Details
                            })
