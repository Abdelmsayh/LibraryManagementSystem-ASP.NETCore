using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Account
{
    public class ResetPasswordDTO
    {
        [Required(ErrorMessage = "password required")]
        [MinLength(6, ErrorMessage = "min length 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [MinLength(6, ErrorMessage = "min length 6")]
        [Compare("Password", ErrorMessage = "password mismatching")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

      
    }
}
