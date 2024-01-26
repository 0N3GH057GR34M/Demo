using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data;

public class ApplicationContext : DbContext
{
  public DbSet<EventEntity> Events { get; set; }
  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    builder.HasDefaultSchema("data");
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}