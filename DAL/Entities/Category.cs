using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(50)"), Required]
        public string CategoryName { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Description { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
