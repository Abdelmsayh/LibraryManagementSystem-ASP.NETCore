using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class BookDTO
    {
        public BookDTO()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            IsActive = true;
            IsAvailable = true;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(50, ErrorMessage = "Author name cannot exceed 50 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [StringLength(100, ErrorMessage = "Book title cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Guid CategoryId { get; set; }

        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        [StringLength(14, MinimumLength = 10, ErrorMessage = "ISBN must be between 10 and 14 characters")]
        public string ISBN { get; set; }

        [Range(1900, 2100, ErrorMessage = "Published year must be between 1900 and 2100")]
        public int PublishedYear { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public int Rating { get; set; }

        [Range(0, int.MaxValue)]
        public int BorrowCount { get; set; }

        [Range(0, int.MaxValue)]
        public int ReservationCount { get; set; }

        public CategoryDTO? Category { get; set; }
    }
}
