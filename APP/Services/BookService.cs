using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class BookService : DbService<Book>, IService<BookRequest, BookResponse>
    {
        public BookService(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Book> DbQuery()
        {
            // Note: SQLite can't order by decimal columns, so Price is not used for ordering.
            return base.DbQuery()
                .Include(b => b.Author)
                .Include(b => b.BookGenres).ThenInclude(bg => bg.Genre)
                .OrderByDescending(b => b.IsTopSeller).ThenByDescending(b => b.PublishDate)
                .ThenBy(b => b.Name);
        }

        public IQueryable<BookResponse> Query()
        {
            return DbQuery().Select(b => new BookResponse
            {
                Id = b.Id,
                Name = b.Name,
                NumberOfPages = b.NumberOfPages,
                PublishDate = b.PublishDate,
                Price = b.Price,
                IsTopSeller = b.IsTopSeller,
                AuthorId = b.AuthorId,

                PublishDateR = b.PublishDate.ToShortDateString(),
                PriceR = b.Price.ToString("C2"),
                IsTopSellerR = b.IsTopSeller ? "Yes" : "No",
                AuthorNameR = b.Author.FirstName + " " + b.Author.LastName,
                GenreNamesR = string.Join("<br>", b.BookGenres.Select(bg => bg.Genre.Name))
            });
        }

        public BookRequest Edit(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return null;
            return new BookRequest
            {
                Id = entity.Id,
                Name = entity.Name,
                NumberOfPages = entity.NumberOfPages,
                PublishDate = entity.PublishDate,
                Price = entity.Price,
                IsTopSeller = entity.IsTopSeller,
                AuthorId = entity.AuthorId,
                GenreIds = entity.GenreIds
            };
        }

        public CommandResponse Add(BookRequest request)
        {
            if (DbQuery().Any(b => b.Name == request.Name.Trim() && b.AuthorId == request.AuthorId))
                return Error("Book with the same name exists for this author!");
            var entity = new Book
            {
                Name = request.Name.Trim(),
                NumberOfPages = request.NumberOfPages,
                PublishDate = request.PublishDate ?? DateTime.Today,
                Price = request.Price ?? 0,
                IsTopSeller = request.IsTopSeller,
                AuthorId = request.AuthorId ?? 0,
                GenreIds = request.GenreIds
            };
            DbAdd(entity);
            return Success(entity.Id, "Book created successfully.");
        }

        public CommandResponse Update(BookRequest request)
        {
            if (DbQuery().Any(b => b.Id != request.Id && b.Name == request.Name.Trim()
                && b.AuthorId == request.AuthorId))
                return Error("Book with the same name exists for this author!");
            var entity = DbSingle(request.Id);
            if (entity is null)
                return Error("Book not found!");
            if (request.GenreIds is not null)
                DbRemove(entity.BookGenres); // delete existing book genres first (many to many)
            entity.Name = request.Name.Trim();
            entity.NumberOfPages = request.NumberOfPages;
            entity.IsTopSeller = request.IsTopSeller;
            if (request.PublishDate.HasValue)
                entity.PublishDate = request.PublishDate.Value;
            if (request.Price.HasValue)
                entity.Price = request.Price.Value;
            if (request.AuthorId.HasValue)
                entity.AuthorId = request.AuthorId.Value;
            if (request.GenreIds is not null)
                entity.GenreIds = request.GenreIds;
            DbUpdate(entity);
            return Success(entity.Id, "Book updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return Error("Book not found!");
            DbRemove(entity.BookGenres); // delete relational book genres first (many to many)
            DbRemove(entity);
            return Success(entity.Id, "Book deleted successfully.");
        }
    }
}
