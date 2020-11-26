using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KAPIAPP.Services.Entity
{
    public class Boutique
    {
        [Column("BoutiqueId")]
        public int Id { get; set; }

        [Display(Name = "Nom")]
        [Required]
        [MaxLength(30, ErrorMessage ="Le nom de la boutique ne doit pas dépassé 30 caractères")]
        public string Name { get; set; }

        [Display(Name = "Ville")]
        [Required]
        [MaxLength(20, ErrorMessage ="le nom de la ville ne doit pas dépassé 20 caractères !")]
        public string City { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }

    }
}
