using Business.Commands;
using FluentValidation;

namespace Business.Validators
{
  public class CreateEventCommandValidator: AbstractValidator<CreateEventCommand>
  {
    public CreateEventCommandValidator()
    {
      RuleFor(command => command.Description)
        .MaximumLength(50)
        .WithMessage("Description length too big.");

      RuleFor(command => command.Date)
        .NotEmpty()
        .Must(time => DateTime.Compare(time, DateTime.Now) > 0)
        .WithMessage("Incorrect date provided.");

      RuleFor(command => command.Type)
        .NotNull()
        .NotEmpty()
        .WithMessage("Incorrect event type provided.");
    }
  }
}
