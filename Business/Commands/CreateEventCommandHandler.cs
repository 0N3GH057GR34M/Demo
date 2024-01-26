using Business.Services;
using Data.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using MediatR;

namespace Business.Commands
{
  public record CreateEventCommand: IRequest
  {
    public EventType Type { get; init; }
    public string? Description { get; init; }
    public DateTime Date { get; init; }
  }
  public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
  {
    private readonly IRepository<EventEntity, Guid> _eventRepository;
    private readonly IDayCheckService<HolidayModel> _weekendCheckService;

    public CreateEventCommandHandler(IRepository<EventEntity, Guid> eventRepository, IDayCheckService<HolidayModel> weekendCheckService)
    {
      ArgumentNullException.ThrowIfNull(eventRepository);
      ArgumentNullException.ThrowIfNull(weekendCheckService);

      _eventRepository = eventRepository;
      _weekendCheckService = weekendCheckService;
    }
    
    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
      ArgumentNullException.ThrowIfNull(request);

      if(!(await _weekendCheckService.CheckAsync(request.Date)))
      {
        await _eventRepository.CreateAsync(new EventEntity
        {
          Description = request.Description,
          Type = request.Type,
          Date = request.Date
        });
      }
    }
  }
}
