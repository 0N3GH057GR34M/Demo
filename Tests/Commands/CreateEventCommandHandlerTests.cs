using Business.Commands;
using Business.Services;
using Data.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Tests.Commands
{
  internal class CreateEventCommandHandlerTests
  {
    private readonly Mock<IRepository<EventEntity, Guid>> _mockRepository = new();
    private readonly Mock<IDayCheckService<HolidayModel>> _mockHolidayCheckService = new();
    private readonly Mock<IValidator<CreateEventCommand>> _mockValidator = new();
    private readonly CreateEventCommandHandler _handler;

    public CreateEventCommandHandlerTests()
    {
      _handler = new CreateEventCommandHandler(
        _mockRepository.Object,
        _mockHolidayCheckService.Object,
        _mockValidator.Object);
    }

    [Test]
    public void HandleAsync_CorrectCommand_ClearResult()
    {
      //Arrange 
      var date = DateTime.UtcNow;
      var line = ToString();

      var command = new CreateEventCommand
      {
        Date = date,
        Description = line,
        Type = EventType.Weekend,
      };

      _mockRepository.Setup(x =>
          x.CreateAsync(It.IsAny<EventEntity>())).Returns(Task.CompletedTask)
          .Verifiable();
      _mockHolidayCheckService.Setup(x =>
          x.CheckAsync(It.IsAny<DateTime>()))
          .ReturnsAsync(It.IsAny<bool>())
          .Verifiable();
      _mockValidator.Setup(x =>
          x.Validate(It.IsAny<CreateEventCommand>()))
          .Returns(new ValidationResult(new ValidationFailure[0]))
          .Verifiable();

      //Act 
      Task task = _handler.Handle(command, CancellationToken.None);

      //Assert 
      _mockHolidayCheckService.VerifyAll();
      _mockRepository.VerifyAll();
      _mockValidator.VerifyAll();
    }
  }
}
