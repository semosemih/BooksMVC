using Microsoft.EntityFrameworkCore;

namespace APP.Domain
{
    public class Db : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
        }
    }
}
