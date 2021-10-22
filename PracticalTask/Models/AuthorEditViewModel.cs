
using System.ComponentModel.DataAnnotations;

namespace PracticalTask.Models
{
    public class AuthorEditViewModel
    {
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(maximumLength: 50, MinimumLength = 0, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} is not valid.")]
        public string PhoneNumber { get; set; }
        public BookViewModel BookViewModel { get; set; }
    }
}
