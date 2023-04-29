global using ShopApp.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopApp.CustomExceptionMiddleware;
using ShopApp.Data;
using ShopApp.Dtos.Customer;
using ShopApp.Dtos.Item;
using ShopApp.Dtos.Order;
using ShopApp.Dtos.Products;
using ShopApp.Dtos.Store;
using ShopApp.Services.CustomerService;
using ShopApp.Services.Generic;
using ShopApp.Services.ImagesService;
using ShopApp.Services.ImagesServices.ImageUploadService;
using ShopApp.Services.ItemService;
using ShopApp.Services.OrderService;
using ShopApp.Services.PrivateKeyService;
using ShopApp.Services.ProductService;
using ShopApp.Services.StockService;
using ShopApp.Services.StoreService;
using ShopApp.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopConnectionString")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
    {
        new OpenApiSecurityScheme
        {
        Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,

        },
        new List<string>()
        }
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(PrivateKeyService.privateKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddScoped<IGenericService<GetProductResponseDto, AddProductRequestDto>, ProductService>();
builder.Services.AddScoped<IGenericService<GetStoreResponseDto, AddStoreRequestDto>, StoreService>();
builder.Services.AddScoped<IGenericService<GetItemResponseDto, AddItemRequestDto>, ItemService>();
builder.Services.AddScoped<IGenericService<GetCustomerResponseDto, AddCustomerRequestDto>, CustomerService>();
builder.Services.AddScoped<IGenericService<GetOrderResponseDto, AddOrderRequestDto>, OrderService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageUploadService, ImageUploadService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUserService, UserService>();

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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Users/Large")),
    RequestPath = "/images/users/large"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Users/Small")),
    RequestPath = "/images/users/small"
});

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
