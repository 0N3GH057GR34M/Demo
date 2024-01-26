using Data;
using Data.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Business.Repositories
{
  public class EventRepository : IRepository<EventEntity, Guid>
  {
    private readonly ApplicationContext _context;
    public EventRepository(ApplicationContext context)
    {
      ArgumentNullException.ThrowIfNull(context);

      _context = context;
    }
    public  async Task CreateAsync(EventEntity entity)
    {
      ArgumentNullException.ThrowIfNull(entity);

      await _context.AddAsync(entity);
      _context.SaveChanges();
    }

    public void Delete(EventEntity entity)
    {
      ArgumentNullException.ThrowIfNull(entity);

      _context.Remove(entity);
      _context.SaveChanges();
    }

    public async Task<IEnumerable<EventEntity>> GetAsync(Expression<Func<EventEntity, bool>>? predicate = null)
    {
      return predicate is null ?
        await _context.Events.ToListAsync() :
        await _context.Events.Where(predicate).ToListAsync();
    }

    public void Update(EventEntity entity)
    {
      ArgumentNullException.ThrowIfNull(entity);

      _context.Update(entity);
      _context.SaveChanges();
    }
  }
}
