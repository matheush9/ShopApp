global using ShopApp.Models;

using Microsoft.EntityFrameworkCore;
using ShopApp.Data;
using ShopApp.Dtos.Products;
using ShopApp.Services.GenericService;
using ShopApp.Services.ProductServices;
using ShopApp.Services.StoreServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopConnectionString")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGenericService<GetProductResponseDto, AddProductRequestDto>, ProductServiceTest>();
builder.Services.AddScoped<IStoreService, StoreService>();

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
