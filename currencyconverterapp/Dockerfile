# ----------------------
# Build Stage
# ----------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application
COPY . ./

# Publish the application
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ----------------------
# Runtime Stage
# ----------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

# Expose port
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "currencyconverterapp.dll"]