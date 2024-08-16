FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5155

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Web-site1/Web-site1.csproj", "Web-site1/"]
RUN dotnet restore "./Web-site1/Web-site1.csproj"
COPY . .
WORKDIR "/src/Web-site1"
RUN dotnet build "./Web-site1.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Web-site1.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

VOLUME /app/.aspnet/DataProtection-Keys

ENTRYPOINT ["dotnet", "Web-site1.dll"]