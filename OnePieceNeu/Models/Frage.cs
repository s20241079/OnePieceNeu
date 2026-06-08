using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePieceNeu.Models
{
    public class Frage
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string Fragentext { get; set; }

        [Required]
        public string AntwortA { get; set; }

        [Required]
        public string AntwortB { get; set; }

        [Required]
        public string AntwortC { get; set; }

        [Required]
        public string AntwortD { get; set; }

        [Required]
        public string KorrekteAntwort { get; set; }

        [Required]
        public string Schwierigkeitsgrad { get; set; }
    }
}
