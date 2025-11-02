using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Extend
{
    public class ApplicationUser : IdentityUser
    {
        [NotMapped]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? PlainPassword { get; set; }


        public Guid? MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; }
        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }
        public bool IsAgree { get; set; }
        public string? ImageName { get; set; }

    }
}
