using KAPIAPP.Services.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KAPIAPP.Services.DTO.EvaluationDto
{
    public class EvaluationToEditDto
    {
        [Column("EvaluationId")]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EvaluationDate { get; set; }

        [Display(Name = "Equipe")]
        [Range(0, 5,
            ErrorMessage = "la note doit être comprise entre 0 et 5")]
        [Required(ErrorMessage = "La notation équipe doit être renseignée...")]
        public int NoteEquipe { get; set; }

        [Display(Name = "Comportement")]
        [Range(0, 5,
            ErrorMessage = "la note doit être comprise entre 0 et 5")]
        [Required(ErrorMessage = "La notation Comportement doit être renseignée...")]
        public int NoteComportement { get; set; }

        [Display(Name = "Propreté")]
        [Range(0, 5,
            ErrorMessage = "la note doit être comprise entre 0 et 5")]
        [Required(ErrorMessage = "La notation Propreté doit être renseignée...")]
        public int NoteProprete { get; set; }

        [Display(Name = "Agencement")]
        [Range(0, 5,
            ErrorMessage = "la note doit être comprise entre 0 et 5")]
        [Required(ErrorMessage = "La notation Agencement doit être renseignée...")]
        public int NoteAgencement { get; set; }

        [Display(Name = "Note Globale")]
        public int NoteGlobale { get; set; }

        public Boutique Boutique { get; set; }
        public User User { get; set; }
    }
}
