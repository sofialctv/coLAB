# Etapa 1: Build da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["colabAPI.csproj", "."]
RUN dotnet restore "./colabAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./colabAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./colabAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
EXPOSE 80

COPY . .

ENTRYPOINT ["dotnet", "colabAPI.dll"]