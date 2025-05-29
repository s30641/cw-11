using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia11.Models
{
    [Table("Prescription")]
    public class Prescription
    {
        [Key]
        public int IdPrescription { get; set; }
        
        public int IdPatient { get; set; }

        [ForeignKey(nameof(IdPatient))]
        public Patient Patient { get; set; }
        
        public int IdDoctor { get; set; }

        [ForeignKey(nameof(IdDoctor))]
        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }
        
        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } 
            = new List<PrescriptionMedicament>();
    }
}