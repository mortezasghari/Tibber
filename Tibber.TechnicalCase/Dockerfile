#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Tibber.TechnicalCase/Tibber.TechnicalCase.csproj", "Tibber.TechnicalCase/"]
COPY ["Tibber.TechnicalCase.Application/Tibber.TechnicalCase.Application.csproj", "Tibber.TechnicalCase.Application/"]
COPY ["Tibber.TechnicalCase.Domain/Tibber.TechnicalCase.Domain.csproj", "Tibber.TechnicalCase.Domain/"]
COPY ["Tibber.TechnicalCase.Infrastructure/Tibber.TechnicalCase.Infrastructure.csproj", "Tibber.TechnicalCase.Infrastructure/"]
COPY ["Tibber.TechnicalCase.Tests/Tibber.TechnicalCase.Tests.csproj", "Tibber.TechnicalCase.Tests/"]

RUN dotnet restore "Tibber.TechnicalCase/Tibber.TechnicalCase.csproj"
RUN dotnet restore "Tibber.TechnicalCase.Tests/Tibber.TechnicalCase.Tests.csproj"

COPY . .
RUN dotnet build "Tibber.TechnicalCase.Tests/Tibber.TechnicalCase.Tests.csproj" -c Release -o /app/build
RUN dotnet test "Tibber.TechnicalCase.Tests/Tibber.TechnicalCase.Tests.csproj" -c Release -o /app/build

WORKDIR "/src/Tibber.TechnicalCase"
RUN dotnet build "Tibber.TechnicalCase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tibber.TechnicalCase.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tibber.TechnicalCase.dll"]