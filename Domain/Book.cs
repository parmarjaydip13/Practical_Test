using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public bool IsDelete { get; set; }

    }
}
