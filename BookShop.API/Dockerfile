#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["BookShop.API/BookShop.API.csproj", "BookShop.API/"]
COPY ["BookShop.Infrastructure/BookShop.Infrastructure.csproj", "BookShop.Infrastructure/"]
COPY ["BookShop.Domain/BookShop.Domain.csproj", "BookShop.Domain/"]
COPY ["BookShop.Common/BookShop.Common.csproj", "BookShop.Common/"]
RUN dotnet restore "BookShop.API/BookShop.API.csproj"
COPY . .
WORKDIR "/src/BookShop.API"
RUN dotnet build "BookShop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.API.dll"]