# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file
COPY ../BookMyMovieApp.sln ./

# Copy the projects
COPY ../BookMyMovieWebServices/ ./BookMyMovieWebServices/
COPY ../BookMyMovieDataAccessLayer/ ./BookMyMovieDataAccessLayer/

# Restore using the solution file
RUN dotnet restore "BookMyMovieApp.sln"

# Build and publish the web service
WORKDIR /src/BookMyMovieWebServices
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "BookMyMovieWebServices.dll"]
