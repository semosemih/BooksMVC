using CORE.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CORE.Services
{
    public abstract class DbService<TEntity> : Service, IDisposable where TEntity : Record, new()
    {
        private readonly DbContext _db;

        protected DbService(DbContext db)
        {
            _db = db;
        }

        protected virtual IQueryable<TEntity> DbQuery()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

        protected TEntity DbSingle(int id)
            => DbQuery().AsTracking().SingleOrDefault(entity => entity.Id == id);

        protected TEntity DbSingle(Expression<Func<TEntity, bool>> predicate) 
            => DbQuery().AsTracking().SingleOrDefault(predicate);

        protected virtual int DbSave() => _db.SaveChanges();

        protected void DbAdd(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Add(entity);
            if (save)
                DbSave();
        }

        protected void DbUpdate(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save)
                DbSave();
        }

        protected void DbRemove(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Remove(entity);
            if (save)
                DbSave();
        }

        protected void DbRemove<TNavigationEntity>(List<TNavigationEntity> navigationEntities) 
            where TNavigationEntity : Record, new() 
        { 
            _db.Set<TNavigationEntity>().RemoveRange(navigationEntities); 
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
