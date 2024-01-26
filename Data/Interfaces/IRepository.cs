using System.Linq.Expressions;

namespace Data.Interfaces
{
  public interface IRepository<T, I>
  {
    Task CreateAsync(T entity);
    void Update(T entity); 
    void Delete(T entity);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? predicate = null);
  }
}
