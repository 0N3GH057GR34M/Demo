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
      if (context is null) throw new ArgumentNullException(nameof(ApplicationContext));

      _context = context;
    }
    public  async Task CreateAsync(EventEntity entity)
    {
      if (entity is null) throw new ArgumentNullException(nameof(EventEntity));

      await _context.AddAsync(entity);
      _context.SaveChanges();
    }

    public async Task DeleteAsync(EventEntity entity)
    {
      if(entity is null) throw new ArgumentNullException(nameof(EventEntity));

      _context.Remove(entity);
      _context.SaveChanges();
    }

    public async Task<IEnumerable<EventEntity>> GetAsync(Expression<Func<EventEntity, bool>>? predicate = null)
    {
      return predicate is not null ?
        await _context.Events.Where(predicate).ToListAsync() :
        await _context.Events.ToListAsync();
    }

    public void Update(EventEntity entity)
    {
      if(entity is null) throw new ArgumentNullException(nameof(EventEntity));

      _context.Update(entity);
      _context.SaveChanges();
    }
  }
}
