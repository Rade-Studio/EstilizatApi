# Etapa base para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia de archivos de proyecto
COPY ["Presentations/WebApi/WebApi.csproj", "Presentations/WebApi/"]
COPY ["Libraries/Caching/Caching.csproj", "Libraries/Caching/"]
COPY ["Libraries/Models/Models.csproj", "Libraries/Models/"]
COPY ["Libraries/Core/Core.csproj", "Libraries/Core/"]
COPY ["Libraries/Data/Data.csproj", "Libraries/Data/"]
COPY ["Libraries/Services/Services.csproj", "Libraries/Services/"]
COPY ["Libraries/Data.Mongo/Data.Mongo.csproj", "Libraries/Data.Mongo/"]
COPY ["Libraries/Identity/Identity.csproj", "Libraries/Identity/"]

# Restauración de dependencias
RUN dotnet restore "./Presentations/WebApi/WebApi.csproj"

# Copia del código fuente
COPY . .

# Construcción del proyecto principal
WORKDIR "/src/Presentations/WebApi"
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicación
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Presentations/WebApi
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final de ejecución
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
