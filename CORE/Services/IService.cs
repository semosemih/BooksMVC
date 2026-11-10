using CORE.Domain;
using CORE.Models;

namespace CORE.Services
{
    public interface IService<TRequest, TResponse>
        where TRequest : Record, new()
        where TResponse : Record, new()
    {
        public IQueryable<TResponse> Query();

        public List<TResponse> List() => Query().ToList();

        public TResponse Single(int id) =>
            Query().SingleOrDefault(response => response.Id == id);

        public TRequest Edit(int id);

        public CommandResponse Add(TRequest request);
        public CommandResponse Update(TRequest request);
        public CommandResponse Remove(int id);
    }
}
