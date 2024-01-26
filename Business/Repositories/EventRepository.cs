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
      if (context is null) throw new ArgumentNullException(nameof(context));

      _context = context;
    }
    public  async Task CreateAsync(EventEntity entity)
    {
      if (entity is null) throw new ArgumentNullException(nameof(entity));

      await _context.AddAsync(entity);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(EventEntity entity)
    {
      if(entity is null) throw new ArgumentNullException(nameof(entity));

      _context.Remove(entity);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<EventEntity>> GetAsync(Expression<Func<EventEntity, bool>>? predicate = null)
    {
      return predicate is not null ?
        await _context.Events.Where(predicate).ToListAsync() :
        await _context.Events.ToListAsync();
    }

    public async Task UpdateAsync(EventEntity entity)
    {
      if(entity is null) throw new ArgumentNullException(nameof(entity));

      _context.Update(entity);
      await _context.SaveChangesAsync();
    }
  }
}
