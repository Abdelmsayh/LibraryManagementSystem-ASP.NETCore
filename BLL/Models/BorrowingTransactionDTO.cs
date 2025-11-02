using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class BorrowingTransactionDTO
    {
        public BorrowingTransactionDTO()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            IsActive = true;
        }

        public Guid Id { get; set; }

        [Required]
        public DateTime BorrowingDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fine amount must be a positive number")]
        public decimal FineAmount { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid MemberId { get; set; }

        public bool IsReturned { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public MemberDTO? Member { get; set; }
    }
}
