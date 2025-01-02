# Use the official ASP.NET Core runtime as the base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY ["BusinessPortal.WebApi/BusinessPortal.WebApi.csproj", "BusinessPortal.WebApi/"]
COPY ["BusinessPortal.Application.Dto/BusinessPortal.Application.Dto.csproj", "BusinessPortal.Application.Dto/"]
COPY ["BusinessPortal.Application.Interface/BusinessPortal.Application.Interface.csproj", "BusinessPortal.Application.Interface/"]
COPY ["BusinessPortal.Application.UseCases/BusinessPortal.Application.UseCases.csproj", "BusinessPortal.Application.UseCases/"]
COPY ["BusinessPortal.Domain/BusinessPortal.Domain.csproj", "BusinessPortal.Domain/"]
COPY ["BusinessPortal.Infrastructure/BusinessPortal.Infrastructure.csproj", "BusinessPortal.Infrastructure/"]
COPY ["BusinessPortal.Persistence/BusinessPortal.Persistence.csproj", "BusinessPortal.Persistence/"]

# Restore dependencies
RUN dotnet restore "BusinessPortal.WebApi/BusinessPortal.WebApi.csproj"

# Copy the remaining source code
COPY . .

# Build the app
WORKDIR "/src/BusinessPortal.WebApi"
RUN dotnet build "BusinessPortal.WebApi.csproj" -c Release -o /app/build

# Publish the app to a folder for the runtime image
FROM build AS publish
RUN dotnet publish "BusinessPortal.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BusinessPortal.WebApi.dll"]
