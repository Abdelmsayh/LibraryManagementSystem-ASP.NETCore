using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Members")]
    public class Member
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)"), Required]
        public string FullName { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2"), Required]
        public DateTime MembershipStartDate { get; set; }

        [Column(TypeName = "datetime2"), Required]
        public DateTime MembershipEndDate { get; set; }

        [Column(TypeName = "nvarchar(20)"), Required]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(50)"), Required, EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public ICollection<ReservationTransaction> ReservationTransactions { get; set; } = new List<ReservationTransaction>();
        public ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();
    }
}
