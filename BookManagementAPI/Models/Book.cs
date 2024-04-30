using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }  // ID is assigned by the server, not input by the user

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [RegularExpression(@"^<p>.*</p>$", ErrorMessage = "Description must be in HTML format.")]
        public string Description { get; set; }

        [Range(1450, 2050, ErrorMessage = "Year must be between 1450 and 2050.")]
        public int Year { get; set; }

        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "ISBN must be a 13-digit number.")]
        public string ISBN { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string AudioURL { get; set; }
    }
}