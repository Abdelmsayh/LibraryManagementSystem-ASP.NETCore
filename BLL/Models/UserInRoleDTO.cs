using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserInRoleDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
