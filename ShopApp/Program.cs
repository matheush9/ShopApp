global using ShopApp.Models;

using Microsoft.EntityFrameworkCore;
using ShopApp.CustomExceptionMiddleware;
using ShopApp.Data;
using ShopApp.Dtos.Customer;
using ShopApp.Dtos.Item;
using ShopApp.Dtos.Order;
using ShopApp.Dtos.Products;
using ShopApp.Dtos.Store;
using ShopApp.Services.CartServices;
using ShopApp.Services.CustomerServices;
using ShopApp.Services.GenericService;
using ShopApp.Services.ItemServices;
using ShopApp.Services.OrderServices;
using ShopApp.Services.ProductServices;
using ShopApp.Services.StockServices;
using ShopApp.Services.StoreServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopConnectionString")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGenericService<GetProductResponseDto, AddProductRequestDto>, ProductService>();
builder.Services.AddScoped<IGenericService<GetStoreResponseDto, AddStoreRequestDto>, StoreService>();
builder.Services.AddScoped<IGenericService<GetItemResponseDto, AddItemRequestDto>, ItemService>();
builder.Services.AddScoped<IGenericService<GetCustomerResponseDto, AddCustomerRequestDto>, CustomerService>();
builder.Services.AddScoped<IGenericService<GetOrderResponseDto, AddOrderRequestDto>, OrderService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
