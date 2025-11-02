using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("ReservationTransactions")]
    public class ReservationTransaction
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "datetime2"), Required]
        public DateTime ReservationDate { get; set; }

        [Column(TypeName = "datetime2"), Required]
        public DateTime DueDate { get; set; }

        public Guid BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }

        public Guid MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public Member? Member { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }
    }
}
