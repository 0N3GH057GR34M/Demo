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
      if(mediator is null) throw new ArgumentNullException(nameof(mediator));
      if(logger is null) throw new ArgumentNullException(nameof(logger));

      _logger = logger;
      _mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
    {
      if (command is null) throw new ArgumentNullException(nameof(CreateEventCommand));

      await _mediator.Send(command);

      return Ok();
    }
  }
}
