using Yarish.University.Filmark.Database;
using Yarish.University.Filmark.Models.Database.Essense;

namespace Yarish.University.Filmark.Database.Interfaces
{
    public interface IDbEntityService<T> where T : DbItem
    {
        Task<T> GetById(int id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);

        IQueryable<T> GetAll();
    }
}