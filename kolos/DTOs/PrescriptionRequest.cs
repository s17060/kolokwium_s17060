using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace kolos.DTOs
{
    public class PrescriptionRequest
    {
        [Required(ErrorMessage = "Date required")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "DueDate required")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "IdPatient required")]
        public int IdPatient { get; set; }
        [Required(ErrorMessage = "IdDoctor required")]
        public int IdDoctor { get; set; }

    }
}
