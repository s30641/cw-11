using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia11.Models
{
    [Table("Prescription_Medicament")]
    public class PrescriptionMedicament
    {
        [Key, Column(Order = 0)]
        public int IdMedicament { get; set; }

        [ForeignKey(nameof(IdMedicament))]
        public Medicament Medicament { get; set; }
        
        [Key, Column(Order = 1)]
        public int IdPrescription { get; set; }

        [ForeignKey(nameof(IdPrescription))]
        public Prescription Prescription { get; set; }

        public int Dose { get; set; }

        [MaxLength(100)]
        public string Details { get; set; }
    }
}