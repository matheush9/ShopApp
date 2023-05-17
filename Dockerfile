FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShopApp/ShopApp.WebAPI.csproj", "ShopApp/"]
COPY ["ShopApp.Application/ShopApp.Application.csproj", "ShopApp.Application/"]
COPY ["ShopApp.Domain/ShopApp.Domain.csproj", "ShopApp.Domain/"]
COPY ["ShopApp.Infrastructure/ShopApp.Infrastructure.csproj", "ShopApp.Infrastructure/"]
RUN dotnet restore "ShopApp/ShopApp.WebAPI.csproj"
COPY . .
WORKDIR "/src/ShopApp"
RUN dotnet build "ShopApp.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopApp.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopApp.WebAPI.dll"]