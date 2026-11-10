using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Genre : Record
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        public List<BookGenre> BookGenres { get; set; } = new(); // navigation property
    }
}
