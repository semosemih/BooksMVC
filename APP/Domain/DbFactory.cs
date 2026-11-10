using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Globalization;

namespace APP.Domain
{
    // Provides a factory for creating Db instances at design time.
    // This is used by Entity Framework (EF) tools such as
    // migrations to construct the database context when the
    // application is not running.
    // This class may need to be created if there are any exceptions
    // during Scaffolding.
    public class DbFactory : IDesignTimeDbContextFactory<Db>
    {
        // The connection string in the configuration file
        // (appsettings.json).
        const string CONNECTIONSTRING = "data source=DB.db";

        // Creates a new instance of the Db type using the connection
        // string. This method is called by EF tooling at design time.
        public Db CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Db>();
            optionsBuilder.UseSqlite(CONNECTIONSTRING);
            return new Db(optionsBuilder.Options);
        }

        // Seeds the database tables with initial data (Optional).
        public void SeedDb()
        {
            // Create a new instance of the Db type:
            var db = CreateDbContext(null);

            // Delete existing data:
            var bookGenres = db.BookGenres.ToList();
            db.BookGenres.RemoveRange(bookGenres);
            var genres = db.Genres.ToList();
            db.Genres.RemoveRange(genres);
            var books = db.Books.ToList();
            db.Books.RemoveRange(books);
            var authors = db.Authors.ToList();
            db.Authors.RemoveRange(authors);
            db.SaveChanges();

            // Reset identity columns (for SQLite):
            // IDs will start from 1
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
                "SET SEQ=0 WHERE NAME='BookGenres'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
                "SET SEQ=0 WHERE NAME='Genres'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
                "SET SEQ=0 WHERE NAME='Books'");
            db.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE " +
                "SET SEQ=0 WHERE NAME='Authors'");

            // Insert new data:
            // Genres:
            db.Genres.Add(new Genre { Name = "Novel" });
            db.Genres.Add(new Genre { Name = "Science Fiction" });
            db.Genres.Add(new Genre { Name = "Dystopia" });
            db.Genres.Add(new Genre { Name = "Classic" });
            db.SaveChanges(); // commit changes to the database

            // Authors with Books:
            db.Authors.Add(new Author
            {
                FirstName = "George",
                LastName = "Orwell",
                Books = new List<Book>
                {
                    new Book
                    {
                        Name = "1984",
                        NumberOfPages = 328,
                        PublishDate = new DateTime(1949, 6, 8),
                        Price = 12.5m,
                        IsTopSeller = true,
                        BookGenres = new List<BookGenre>
                        {
                            new BookGenre { GenreId = db.Genres.Single(g => g.Name == "Novel").Id },
                            new BookGenre { GenreId = db.Genres.Single(g => g.Name == "Dystopia").Id }
                        }
                    },
                    new Book
                    {
                        Name = "Animal Farm",
                        NumberOfPages = 112,
                        PublishDate = DateTime.Parse("08/17/1945", new CultureInfo("en-US")),
                        Price = 8.75m,
                        IsTopSeller = false,
                        BookGenres = new List<BookGenre>
                        {
                            new BookGenre { GenreId = db.Genres.Single(g => g.Name == "Classic").Id }
                        }
                    }
                }
            });
            db.Authors.Add(new Author
            {
                FirstName = "Isaac",
                LastName = "Asimov",
                Books = new List<Book>
                {
                    new Book
                    {
                        Name = "Foundation",
                        PublishDate = new DateTime(1951, 5, 1),
                        Price = 15m,
                        IsTopSeller = true,
                        BookGenres = new List<BookGenre>
                        {
                            new BookGenre { GenreId = db.Genres.Single(g => g.Name == "Science Fiction").Id }
                        }
                    }
                }
            });
            db.Authors.Add(new Author { FirstName = "Orhan", LastName = "Pamuk" });
            db.SaveChanges();
        }
    }
}
