using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserDTO
    {
        public UserDTO()
        {
            CreatedOn = DateTime.Now;
            IsActive = true;
        }

        public String Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public String?  PhoneNumber { get; set; }

        [Required]
        public Guid? MemberId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? ImageName { get; set; }
        public IFormFile Image { get; set; }

        public List<UserInRoleDTO> UserRoles { get; set; } = new List<UserInRoleDTO>();
    }
}
