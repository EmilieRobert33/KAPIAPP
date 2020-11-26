using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KAPIAPP.Services.DTO.BoutiqueDto
{
    public class BoutiqueEditDto
    {
        public int Id { get; set; }
        [Display(Name = "Nom")]
        [Required]
        [MaxLength(30, ErrorMessage = "Le nom de la boutique ne doit pas dépasser 30 caractères")]
        public string Name { get; set; }
        [Display(Name = "Ville")]
        [Required]
        [MaxLength(30, ErrorMessage = "La ville de la boutique ne doit pas dépasser 30 caractères")]
        public string City { get; set; }
    }
}
