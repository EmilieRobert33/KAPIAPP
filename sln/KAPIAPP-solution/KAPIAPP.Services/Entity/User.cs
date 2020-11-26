using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KAPIAPP.Services.Entity
{
    public class User : IdentityUser
    {
        //[Column("UserId")]
        //public override string Id { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }
        [Display(Name = "Nom")]
        public string LastName { get; set; }
        public string Poste { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
