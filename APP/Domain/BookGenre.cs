using CORE.Domain;

namespace APP.Domain
{
    public class BookGenre : Record
    {
        public int BookId { get; set; } // foreign key
        public Book Book { get; set; } // navigation property
        public int GenreId { get; set; } // foreign key
        public Genre Genre { get; set; } // navigation property
    }
}
