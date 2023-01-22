global using ShopApp.Models;
global using ShopApp.Services;

using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Product;
using ShopApp.Services.ResponseHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopConnectionString")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IResponseHandler<GetProductResponseDto>, ProductResponseHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
