using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class AuthorService : DbService<Author>, IService<AuthorRequest, AuthorResponse>
    {
        public AuthorService(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Author> DbQuery()
        {
            return base.DbQuery()
                .Include(a => a.Books)
                .OrderBy(a => a.FirstName).ThenBy(a => a.LastName);
        }

        public IQueryable<AuthorResponse> Query()
        {
            return DbQuery().Select(a => new AuthorResponse
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                FullNameR = a.FirstName + " " + a.LastName,
                BookCountR = a.Books.Count,
                BookNamesR = string.Join("<br>", a.Books.Select(b => b.Name))
            });
        }

        public AuthorRequest Edit(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return null;
            return new AuthorRequest
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }

        public CommandResponse Add(AuthorRequest request)
        {
            if (DbQuery().Any(a => a.FirstName == request.FirstName.Trim() && a.LastName == request.LastName.Trim()))
                return Error("Author with the same first and last name exists!");
            var entity = new Author
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim()
            };
            DbAdd(entity);
            return Success(entity.Id, "Author created successfully.");
        }

        public CommandResponse Update(AuthorRequest request)
        {
            if (DbQuery().Any(a => a.Id != request.Id && a.FirstName == request.FirstName.Trim()
                && a.LastName == request.LastName.Trim()))
                return Error("Author with the same first and last name exists!");
            var entity = DbSingle(request.Id);
            if (entity is null)
                return Error("Author not found!");
            entity.FirstName = request.FirstName.Trim();
            entity.LastName = request.LastName.Trim();
            DbUpdate(entity);
            return Success(entity.Id, "Author updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return Error("Author not found!");
            // Check if the Author entity has relational Book entities.
            if (entity.Books.Any())
                return Error("Author has relational books!");
            DbRemove(entity);
            return Success(entity.Id, "Author deleted successfully.");
        }
    }
}
