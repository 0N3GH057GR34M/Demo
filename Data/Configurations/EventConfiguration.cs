using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
  public class EventConfiguration: IEntityTypeConfiguration<EventEntity>
  {
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
      builder.ToTable("events");
      builder.HasKey(x => x.Id);
      builder.Property(x => x.Type).IsRequired().HasColumnName("type");
      builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(50);
      builder.Property(x => x.Date).IsRequired().HasColumnName("date");
    }
  }
}
