using System;
using System.ComponentModel.DataAnnotations;

namespace PracticalTask.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 150, MinimumLength = 0, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string PublishDate { get; set; }

        public DateTime PublishDateDt { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public int? Quantity { get; set; }

        public int AuthorId { get; set; }
    }
}
