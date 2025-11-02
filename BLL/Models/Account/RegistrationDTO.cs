using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Account
{
    public class RegistrationDTO
    {
        [Required(ErrorMessage = "full name required")]
        [MinLength(3, ErrorMessage = "min len 3 character")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "email required")]
        [EmailAddress(ErrorMessage = "invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password required")]
        [MinLength(6, ErrorMessage = "min length 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [MinLength(6, ErrorMessage = "min length 6")]
        [Compare("Password", ErrorMessage = "password mismatching")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }

        public String? Phone { get; set; }
    }
}
