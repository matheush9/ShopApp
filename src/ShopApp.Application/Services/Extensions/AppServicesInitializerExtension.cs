using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopApp.Application.Authorization.Handlers;
using ShopApp.Application.Authorization.Requirements;
using ShopApp.Application.Interfaces.Customer;
using ShopApp.Application.Interfaces.Images;
using ShopApp.Application.Interfaces.Images.ImageUploadService;
using ShopApp.Application.Interfaces.Item;
using ShopApp.Application.Interfaces.Order;
using ShopApp.Application.Interfaces.ProductCategory;
using ShopApp.Application.Interfaces.ProductService;
using ShopApp.Application.Interfaces.Stock;
using ShopApp.Application.Interfaces.Store;
using ShopApp.Application.Interfaces.User;
using ShopApp.Application.Services.CustomerServices;
using ShopApp.Application.Services.Images;
using ShopApp.Application.Services.Images.ImageUploadService;
using ShopApp.Application.Services.ItemServices;
using ShopApp.Application.Services.OrderServices;
using ShopApp.Application.Services.PrivateKey;
using ShopApp.Application.Services.ProductCategory;
using ShopApp.Application.Services.ProductServices;
using ShopApp.Application.Services.StockServices;
using ShopApp.Application.Services.StoreServices;
using ShopApp.Application.Services.UserServices;
using System.Reflection;

namespace ShopApp.Application.Services.Extensions
{
    public static partial class AppServicesInitializerExtension
    {
        public static IServiceCollection RegisterApplicationServices(
                                            this IServiceCollection services)
        {
            RegisterCustomServices(services);
            RegisterAuthentication(services);
            RegisterAuthorization(services);
            RegisterSwagger(services);
            RegisterMapper(services);

            return services;
        }

        public static void RegisterCustomServices(IServiceCollection services)
        {
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddTransient<IAuthorizationHandler, UserAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, CustomerAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, StoreAuthorizationHandler>(); 
            services.AddTransient<IAuthorizationHandler, OrderAuthorizationHandler>();
        }

        public static void RegisterAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(x =>
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
        }

        public static void RegisterAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", policy => policy.Requirements.Add(new UserRequirement()));
                options.AddPolicy("CustomerPolicy", policy => policy.Requirements.Add(new CustomerRequirement()));
                options.AddPolicy("StorePolicy", policy => policy.Requirements.Add(new StoreRequirement()));
                options.AddPolicy("OrderPolicy", policy => policy.Requirements.Add(new OrderRequirement()));
            });
        }

        public static void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                  new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                      },
                      Scheme = "oauth2",
                      Name = "Bearer",
                      In = ParameterLocation.Header,

                  },
                  new List < string > ()
                }
              });
            });
        }

        public static void RegisterMapper(IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
