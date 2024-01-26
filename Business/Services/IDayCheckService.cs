namespace Business.Services
{
  public interface IDayCheckService<T>
  {
    Task<bool> CheckAsync(DateTime date);
  }
}
