using Common.DAL.Context;
using Common.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.AddSingleton(typeof(ITableRepository), typeof(TableRepository));
services.AddSingleton(typeof(IRestaurantRepository), typeof(RestaurantRepository));
services.AddSingleton(typeof(IContextDB), typeof(ContextDB));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
