using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KAPIAPP.Services.DTO
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de Passe")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir")]
        public bool RememberMe { get; set; }
    }
}
