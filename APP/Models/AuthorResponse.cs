using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class AuthorResponse : Record
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Full Name")]
        public string FullNameR { get; set; }

        [DisplayName("Book Count")]
        public int BookCountR { get; set; }

        [DisplayName("Books")]
        public string BookNamesR { get; set; }
    }
}
