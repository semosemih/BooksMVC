using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Author : Record
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        public List<Book> Books { get; set; } = new(); // navigation property
    }
}
