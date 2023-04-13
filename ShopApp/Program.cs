global using ShopApp.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ShopApp.CustomExceptionMiddleware;
using ShopApp.Data;
using ShopApp.Dtos.Customer;
using ShopApp.Dtos.Item;
using ShopApp.Dtos.Order;
using ShopApp.Dtos.Products;
using ShopApp.Dtos.Store;
using ShopApp.Services.CustomerServices;
using ShopApp.Services.GenericService;
using ShopApp.Services.ImagesServices;
using ShopApp.Services.ImagesServices.ImageUploadService;
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
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
builder.Services.AddScoped<IImageService, ImageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products/Large")),
    RequestPath = "/images/products/large"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products/Small")),
    RequestPath = "/images/products/small"
});

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
