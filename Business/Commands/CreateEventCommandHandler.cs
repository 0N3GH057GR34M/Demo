using Data.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Business.Commands
{
  public record CreateEventCommand: IRequest
  {
    public EventType Type { get; init; }
    public string? Description { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
  }
  public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
  {
    private readonly IRepository<EventEntity, Guid> _eventRepository;

    public CreateEventCommandHandler(IRepository<EventEntity, Guid> eventRepository)
    {
      if(eventRepository is null) throw new ArgumentNullException(nameof(IRepository<EventEntity, Guid>));

      _eventRepository = eventRepository;
    }
    
    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
      if(request is null) throw new ArgumentNullException(nameof(CreateEventCommand));

      await _eventRepository.CreateAsync(new EventEntity
      {
        Description = request.Description,
        Type = request.Type,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
      });
    }
  }
}
