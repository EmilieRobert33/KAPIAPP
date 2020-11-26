using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KAPIAPP.Services.DTO
{
    public class ResetPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Le mot de passe doit correspondre...")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
