using Business.Services;
using Domain;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;

namespace Business.Implementations
{
  public class WeekendCheckService: IDayCheckService<HolidayModel>
  {
    private readonly IHttpClientFactory _httpClientFactory;
    public WeekendCheckService(IHttpClientFactory httpClientFactory)
    {
      ArgumentNullException.ThrowIfNull(httpClientFactory);

      _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> CheckAsync(DateTime date)
    {
      var url = $"{Constants.HolidayCheckApiUrl}{DateTime.Now.Year}/{Constants.PolandCountryCode}";
      HolidayModel[] holidays;
      using (HttpClient client = _httpClientFactory.CreateClient())
      {
        try
        {
          holidays = await client.GetFromJsonAsync<HolidayModel[]>(url, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? [];
        }
        catch
        {
          throw;
        }
      }

      return 
        holidays.Any(item => item.Date.Equals(date)) ||
        date.DayOfWeek == DayOfWeek.Saturday ||
        date.DayOfWeek == DayOfWeek.Sunday;
    }
  }
}
