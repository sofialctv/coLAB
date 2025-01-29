# Etapa 1: Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa 2: Build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar apenas o arquivo do projeto para restaurar dependências
COPY ["colabAPI.csproj", "./"]
RUN dotnet restore "colabAPI.csproj"

# Copiar todo o código-font após o restore
COPY . .

# Build da aplicação
RUN dotnet build "colabAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa 3: Publicação da aplicação
FROM build AS publish
RUN dotnet publish "colabAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa 4: Criando a imagem final com a aplicação publicada
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Comando de inicialização
ENTRYPOINT ["dotnet", "colabAPI.dll"]
