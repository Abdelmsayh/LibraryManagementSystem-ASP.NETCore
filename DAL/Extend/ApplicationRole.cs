using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extend
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
            IsActive = true;
            CreatedOn = DateTime.Now;
        }

        [Column(TypeName = "nvarchar(250)")]
        public string? Description { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }
    }
}
