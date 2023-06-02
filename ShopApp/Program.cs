using Microsoft.Extensions.FileProviders;
using ShopApp.Application.Services.Extensions;
using ShopApp.Infrastructure.CustomMiddlewares.CustomExceptionMiddleware;
using ShopApp.Infrastructure.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterDataServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterApplicationServices();

var app = builder.Build();

app.MigrateDatabase();

app.UseSwagger();
app.UseSwaggerUI();

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

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
