using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)"), Required]
        public string Author { get; set; }

        [Column(TypeName = "nvarchar(100)"), Required]
        public string Title { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public bool IsAvailable { get; set; }

        [Column(TypeName = "nvarchar(14)"), Required]
        public string ISBN { get; set; }


        public int PublishedYear { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }
        public int Rating { get; set; }

        public ICollection<BorrowingTransaction> BorrowingTransactions { get; set; } = new List<BorrowingTransaction>();

        public ICollection<ReservationTransaction> ReservationTransactions { get; set; } = new List<ReservationTransaction>();
    }
}
