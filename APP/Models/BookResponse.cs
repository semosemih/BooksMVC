using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class BookResponse : Record
    {
        public string Name { get; set; }

        [DisplayName("Number of Pages")]
        public short? NumberOfPages { get; set; }

        public DateTime PublishDate { get; set; }

        public decimal Price { get; set; }

        public bool IsTopSeller { get; set; }

        public int AuthorId { get; set; }

        public List<int> GenreIds { get; set; }

        [DisplayName("Publish Date")]
        public string PublishDateR { get; set; }

        [DisplayName("Price")]
        public string PriceR { get; set; }

        [DisplayName("Top Seller")]
        public string IsTopSellerR { get; set; }

        [DisplayName("Author")]
        public string AuthorNameR { get; set; }

        [DisplayName("Genres")]
        public string GenreNamesR { get; set; }
    }
}
