using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class ReservationTransactionDTO
    {
        public ReservationTransactionDTO()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            IsActive = true;
        }

        public Guid Id { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public Guid BookId { get; set; }

        public BookDTO Book { get; set; }

        [Required]
        public Guid MemberId { get; set; }
        public MemberDTO Member { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
