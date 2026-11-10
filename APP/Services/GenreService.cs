using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class GenreService : DbService<Genre>, IService<GenreRequest, GenreResponse>
    {
        public GenreService(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Genre> DbQuery()
        {
            return base.DbQuery()
                .Include(g => g.BookGenres).ThenInclude(bg => bg.Book)
                .OrderBy(g => g.Name);
        }

        public IQueryable<GenreResponse> Query()
        {
            return DbQuery().Select(g => new GenreResponse
            {
                Id = g.Id,
                Name = g.Name,
                BookCountR = g.BookGenres.Count,
                BookNamesR = string.Join("<br>", g.BookGenres.Select(bg => bg.Book.Name))
            });
        }

        public GenreRequest Edit(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return null;
            return new GenreRequest
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public CommandResponse Add(GenreRequest request)
        {
            if (DbQuery().Any(g => g.Name == request.Name.Trim()))
                return Error("Genre with the same name exists!");
            var entity = new Genre
            {
                Name = request.Name.Trim()
            };
            DbAdd(entity);
            return Success(entity.Id, "Genre created successfully.");
        }

        public CommandResponse Update(GenreRequest request)
        {
            if (DbQuery().Any(g => g.Id != request.Id && g.Name == request.Name.Trim()))
                return Error("Genre with the same name exists!");
            var entity = DbSingle(request.Id);
            if (entity is null)
                return Error("Genre not found!");
            entity.Name = request.Name.Trim();
            DbUpdate(entity);
            return Success(entity.Id, "Genre updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var entity = DbSingle(id);
            if (entity is null)
                return Error("Genre not found!");
            DbRemove(entity.BookGenres); // delete relational book genres first (many to many)
            DbRemove(entity);
            return Success(entity.Id, "Genre deleted successfully.");
        }
    }
}
