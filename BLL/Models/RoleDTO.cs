using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class RoleDTO
    {
        public RoleDTO()
        {
            CreatedOn = DateTime.Now;
            IsActive = true;
        }

        public String Id { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        [StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters")]
        public string RoleName { get; set; }

        [StringLength(250, ErrorMessage = "Role Description cannot exceed 250 characters")]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<UserInRoleDTO> UserRoles { get; set; } = new List<UserInRoleDTO>();
    }
}
