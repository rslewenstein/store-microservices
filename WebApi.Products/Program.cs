using AutoMapper;
using WebApi.Products.Application;
using WebApi.Products.Application.Interfaces;
using WebApi.Products.Infrastructure.Data;
using WebApi.Products.Infrastructure.Data.Interfaces;
using WebApi.Products.Infrastructure.Helper;
using WebApi.Products.Infrastructure.Messaging;
using WebApi.Products.Infrastructure.Messaging.Interfaces;
using WebApi.Products.Infrastructure.Repository;
using WebApi.Products.Infrastructure.Repository.Interfaces;
using WebApi.Products.Infrastructure.Worker;

var builder = WebApplication.CreateBuilder(args);

// Logger config
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Register the interfaces
builder.Services.AddScoped<ProductContext>();
builder.Services.AddSingleton<ICreateConnection, CreateConnection>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IMessageConnection, MessageConnection>();

// Calling worker
builder.Services.AddHostedService<ProductWorker>();

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
    var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
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
