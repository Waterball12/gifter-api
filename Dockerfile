#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gifter.Api/Gifter.Api.csproj", "Gifter.Api/"]
COPY ["Gifter.Domain/Gifter.Domain.csproj", "Gifter.Domain/"]
COPY ["Gifter.Infrastructure/Gifter.Infrastructure.csproj", "Gifter.Infrastructure/"]
RUN dotnet restore "Gifter.Api/Gifter.Api.csproj"
COPY . .
WORKDIR "/src/Gifter.Api"
RUN dotnet build "Gifter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gifter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gifter.Api.dll"]