using AutoMapper;
using WebApi.Chart.Application;
using WebApi.Chart.Application.Interfaces;
using WebApi.Chart.Domain.Helper;
using WebApi.Chart.Infrastructure.Data;
using WebApi.Chart.Infrastructure.Data.Interfaces;
using WebApi.Chart.Infrastructure.Repository;
using WebApi.Chart.Infrastructure.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register the interfaces
builder.Services.AddScoped<ChartContext>();
builder.Services.AddScoped<ICreateConnection, CreateConnection>();
builder.Services.AddScoped<IChartService, ChartService>();
builder.Services.AddScoped<IChartRepository, ChartRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(map =>
{
    map.AddProfile(new MapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);  

var app = builder.Build();

// Setup Dapper
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ChartContext>();
    await context.Init();
}

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
