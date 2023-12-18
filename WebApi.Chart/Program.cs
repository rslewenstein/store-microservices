using WebApi.Chart.Infrastructure.Data;
using WebApi.Chart.Infrastructure.Data.Interfaces;

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
