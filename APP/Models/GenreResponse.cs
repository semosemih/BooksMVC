using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class GenreResponse : Record
    {
        public string Name { get; set; }

        [DisplayName("Book Count")]
        public int BookCountR { get; set; }

        [DisplayName("Books")]
        public string BookNamesR { get; set; }
    }
}
