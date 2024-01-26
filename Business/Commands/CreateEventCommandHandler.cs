using Business.Services;
using Data.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    private readonly IValidator<CreateEventCommand> _validator;

    public CreateEventCommandHandler(
      IRepository<EventEntity, Guid> eventRepository,
      IDayCheckService<HolidayModel> weekendCheckService,
      IValidator<CreateEventCommand> validator)
    {
      ArgumentNullException.ThrowIfNull(eventRepository);
      ArgumentNullException.ThrowIfNull(weekendCheckService);
      ArgumentNullException.ThrowIfNull(validator);

      _eventRepository = eventRepository;
      _weekendCheckService = weekendCheckService;
      _validator = validator;
    }
    
    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
      ArgumentNullException.ThrowIfNull(request);

      var validationResult = _validator.Validate(request);
      if (!validationResult.IsValid)
      {
        throw new ValidationException(validationResult.Errors.First().ErrorMessage);
      }

      if (!(await _weekendCheckService.CheckAsync(request.Date)))
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
