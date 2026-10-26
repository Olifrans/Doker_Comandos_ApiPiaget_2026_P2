# Fase 1: Base para execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Ajuste o caminho abaixo se seu .csproj estiver dentro de uma pasta (ex: ApiPiagetDois/ApiPiagetDois.csproj)
COPY ["ApiPiagetDois.csproj", "."] 
RUN dotnet restore "./ApiPiagetDois.csproj"
COPY . .
RUN dotnet build "./ApiPiagetDois.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase 3: Publish
FROM build AS publish
RUN dotnet publish "./ApiPiagetDois.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase 4: Imagem Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiPiagetDois.dll"]


