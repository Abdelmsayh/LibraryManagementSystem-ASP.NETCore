using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Account
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "email required")]
        [EmailAddress(ErrorMessage = "invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password required")]
        [MinLength(6, ErrorMessage = "min length 6")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
