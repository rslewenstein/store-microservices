using AutoMapper;
using WebApi.ShoppingCart.Application;
using WebApi.ShoppingCart.Application.Interfaces;
using WebApi.ShoppingCart.Domain.Helper;
using WebApi.ShoppingCart.Infrastructure.Data;
using WebApi.ShoppingCart.Infrastructure.Data.Interfaces;
using WebApi.ShoppingCart.Infrastructure.Repository;
using WebApi.ShoppingCart.Infrastructure.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Register the interfaces
builder.Services.AddScoped<ShoppingCartContext>();
builder.Services.AddScoped<ICreateConnection, CreateConnection>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

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
    var context = scope.ServiceProvider.GetRequiredService<ShoppingCartContext>();
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
