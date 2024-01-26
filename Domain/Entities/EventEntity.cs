using Domain.Enums;

namespace Domain.Entities
{
  public class EventEntity
  {
    public Guid Id { get; set; }
    public EventType Type { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set;}
  }
}