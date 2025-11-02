using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class MemberDTO
    {
        public MemberDTO()
        {
            Id = Guid.NewGuid();
            IsActive = true;
            MembershipStartDate = DateTime.Now;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public DateTime MembershipStartDate { get; set; }

        public DateTime MembershipEndDate { get; set; }

        public ICollection<ReservationTransactionDTO>? ReservationTransactions { get; set; }
        public ICollection<BorrowingTransactionDTO>? BorrowingTransactions { get; set; }
    }
}
