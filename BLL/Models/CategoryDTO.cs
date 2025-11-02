using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.Now;
            IsActive = true;
        }

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
        public string CategoryName { get; set; }

        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
    }
}
