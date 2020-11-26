using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KAPIAPP.Services.DTO
{
    public class TwoStepDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string TwoFactorCode { get; set; }
        public bool RememberMe { get; set; }
    }
}
