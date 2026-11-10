using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class BookRequest : Record
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Name { get; set; }

        [DisplayName("Number of Pages")]
        [Range(1, short.MaxValue, ErrorMessage = "{0} must be between {1} and {2}!")]
        public short? NumberOfPages { get; set; }

        [DisplayName("Publish Date")]
        [Required(ErrorMessage = "{0} is required!")]
        public DateTime? PublishDate { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [Range(0.0, 1000000.0, ErrorMessage = "{0} must be between {1} and {2}!")]
        public decimal? Price { get; set; }

        [DisplayName("Top Seller")]
        public bool IsTopSeller { get; set; }

        [DisplayName("Author")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? AuthorId { get; set; }

        [DisplayName("Genres")]
        [Required(ErrorMessage = "{0} is required!")]
        public List<int> GenreIds { get; set; }
    }
}
