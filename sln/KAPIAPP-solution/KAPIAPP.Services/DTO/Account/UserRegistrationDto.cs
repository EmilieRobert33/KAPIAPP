using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KAPIAPP.Services.DTO
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "email obligatoire")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "mdp obligatoire")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "le mdp et sa confirmation doivent être identique")]
        public string ConfirmPassword { get; set; }
    }
}
