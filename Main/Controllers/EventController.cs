using Business.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Main.Controllers
{
  [ApiController]
  [Route("event")]
  public class EventController : ControllerBase
  {
    private readonly IMediator _mediator;

    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger, IMediator mediator)
    {
      ArgumentNullException.ThrowIfNull(mediator);
      ArgumentNullException.ThrowIfNull(logger);

      _logger = logger;
      _mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
    {
      ArgumentNullException.ThrowIfNull(command);

      await _mediator.Send(command);

      _logger.LogInformation("Event added succesfuly.");

      return Ok();
    }
  }
}
