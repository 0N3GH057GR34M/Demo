using Business.Commands;
using Business.Implementations;
using Business.Services;
using Data.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Models;
using FluentValidation;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
  internal class WeekendCheckServiceTests
  {
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory = new();
    private readonly Mock<HttpClient> _mockHttpClient = new();
    private readonly WeekendCheckService _service;

    public WeekendCheckServiceTests()
    {
      _service = new WeekendCheckService(
        _mockHttpClientFactory.Object);
    }

    [Test]
    public void CheckAsync_WeekendDate_TrueResult()
    {
      //Arrange

      _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(_mockHttpClient.Object);

      var date = new DateTime(2024, 1, 27);

      var holiday = new HolidayModel
      {
        Date = date,
      };

      //Act 
      var result = _service.CheckAsync(date).Result;

      //Assert 
      _mockHttpClientFactory.VerifyAll();
      _mockHttpClient.VerifyAll();

      Assert.IsTrue(result);
    }
  }
}
