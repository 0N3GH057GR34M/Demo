using System.Linq.Expressions;

namespace Data.Interfaces
{
  public interface IRepository<T, I>
  {
    Task CreateAsync(T entity);
    void Update(T entity); 
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? predicate = null);
  }
}
