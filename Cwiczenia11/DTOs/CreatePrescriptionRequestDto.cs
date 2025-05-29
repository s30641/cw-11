namespace Cwiczenia11.DTOs
{
    public class CreatePrescriptionRequestDto
    {
        public PatientDto Patient { get; set; }
        public List<MedicamentPrescriptionDto> Medicaments { get; set; }
        public int IdDoctor { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class PatientDto
    {
        public int? IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class MedicamentPrescriptionDto
    {
        public int IdMedicament { get; set; }
        public int Dose { get; set; }
        public string Description { get; set; }
    }
}