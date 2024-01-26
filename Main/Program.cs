using Business.Repositories;
using Data;
using Data.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRepository<EventEntity, Guid>, EventRepository>();

builder.Services.AddDbContextPool<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlServer")));

var app = builder.Build();

app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>().Database.Migrate();

app.UseCors(builder => {
  builder
  .WithMethods(app.Configuration.GetSection("CorsOptions:AllowedMethods").Value)
  .WithHeaders(app.Configuration.GetSection("CorsOptions:AllowedHeaders").Value)
  .WithOrigins(app.Configuration.GetSection("CorsOptions:AllowedOrigins").Value);
});


if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
