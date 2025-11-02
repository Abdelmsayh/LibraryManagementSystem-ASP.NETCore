using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("BorrowingTransactions")]
    public class BorrowingTransaction
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "datetime2"), Required]
        public DateTime BorrowingDate { get; set; }

        [Column(TypeName = "datetime2"), Required]
        public DateTime DueDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ReturnDate { get; set; }

        [Column(TypeName = "decimal(18,2)"), Required]
        public decimal? FineAmount { get; set; }

        public Guid BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }

        public Guid MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public Member? Member { get; set; }

        public bool IsReturned { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
