﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopApp.Application.Interfaces.Generic;
using ShopApp.Application.Interfaces.Images;
using ShopApp.Application.Interfaces.Images.ImageUploadService;
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
using ShopApp.Domain.DTOs.Customer;
using ShopApp.Domain.DTOs.Item;
using ShopApp.Domain.DTOs.Order;
using ShopApp.Domain.DTOs.Products;
using ShopApp.Domain.DTOs.Store;
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
            RegisterSwagger(services);
            RegisterMapper(services);

            return services;
        }

        public static void RegisterCustomServices(IServiceCollection services)
        {
            services.AddScoped<IGenericService<GetProductResponseDto, AddProductRequestDto>, ProductService>();
            services.AddScoped<IGenericService<GetStoreResponseDto, AddStoreRequestDto>, StoreService>();
            services.AddScoped<IGenericService<GetItemResponseDto, AddItemRequestDto>, ItemService>();
            services.AddScoped<IGenericService<GetCustomerResponseDto, AddCustomerRequestDto>, CustomerService>();
            services.AddScoped<IGenericService<GetOrderResponseDto, AddOrderRequestDto>, OrderService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
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
