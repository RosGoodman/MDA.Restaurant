using Common.DAL.Context;
using Common.DAL.Repositories;
using MDA.Restaurant.Jobs;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "SimpleSwagger.xml");
    c.IncludeXmlComments(filePath, includeControllerXmlComments: true);
});

services.AddSingleton(typeof(ITableRepository), typeof(TableRepository));
services.AddSingleton(typeof(IRestaurantRepository), typeof(RestaurantRepository));
services.AddSingleton(typeof(IContextDB), typeof(ContextDB));

//���������� �������� Job
services.AddSingleton<IJobFactory, SingletonJobFactory>();
services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
//���������� �����
services.AddSingleton<RemovingTheReservationsJob>();
services.AddSingleton(new JobSchedule(
    jobType: typeof(RemovingTheReservationsJob),
    cronExpression: "0/20 * * * * ?"));
//����������� �������
services.AddHostedService<QuartzHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
